using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Models.WebSocketContent;
using RedditFreeGamesNotifier.Strings;
using System.Net.WebSockets;
using System.Text.Json;
using Websocket.Client;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class QQWebSocket(ILogger<QQWebSocket> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<QQWebSocket> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notifications to QQ WebSocket";
		private readonly string debugWSReconnection = "Reconnection happened, type: {0}";
		private readonly string debugWSMessageRecieved = "Message received: {0}";
		private readonly string debugWSDisconnected = "Disconnected: {0}";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var packets = GetSendPacket(config, records);

				using var client = GetWSClient(config);

				await client.Start();

				foreach (var packet in packets) {
					await client.SendInstant(JsonSerializer.Serialize(packet));
					await Task.Delay(600);
				}

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(debugSendMessage);

				var packet = new WSPacket {
					Action = NotifyFormatStrings.qqWebSocketSendAction,
					Params = new Param {
						UserID = config.ToQQID,
						Message = asfResult
					}
				};

				using var client = GetWSClient(config);

				await client.Start();

				await client.SendInstant(JsonSerializer.Serialize(packet));
				await Task.Delay(500);

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		private WebsocketClient GetWSClient(NotifyConfig config) {
			var url = new Uri(string.Format(NotifyFormatStrings.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(debugWSReconnection, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(debugWSMessageRecieved, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(debugWSDisconnected, msg));
			#endregion

			return client;
		}

		private static List<WSPacket> GetSendPacket(NotifyConfig config, List<NotifyRecord> records) {
			return records.Select(record => new WSPacket() {
				Action = NotifyFormatStrings.qqWebSocketSendAction,
				Params = new Param {
					UserID = config.ToQQID,
					Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}"
				}
			}).ToList();
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
