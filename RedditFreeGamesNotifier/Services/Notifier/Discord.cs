using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.PostContent;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class Discord: INotifiable {
		private readonly ILogger<Discord> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Discord";
		private readonly string debugSendMessageASF = "Send ASF result to Discord";
		private readonly string debugGeneratePostContent = "Generating Discord POST content";
		#endregion

		public Discord(ILogger<Discord> logger) {
			_logger = logger;
		}

		private List<DiscordPostContent> GeneratePostContent(List<NotifyRecord> records) {
			_logger.LogDebug(debugGeneratePostContent);

			var contents = new List<DiscordPostContent>();
			var content = new DiscordPostContent() {
				Content = records.Count > 1 ? "New Free Games - Reddit" : "New Free Game - Reddit"
			};

			for (int i = 0; i < records.Count; i++) {
				if (content.Embeds.Count == 10) {
					contents.Add(content);
					content = new DiscordPostContent() {
						Content = records.Count - i - 1 > 1 ? "New Free Games - Reddit" : "New Free Game - Reddit"
					};
				}

				content.Embeds.Add(
					new Embed() {
						Title = records[i].Name,
						Url = records[i].Url,
						Description = records[i].ToDiscordMessage(),
						Footer = new Footer() {
							Text = NotifyFormatStrings.projectLink
						}
					}
				);

				if(i == records.Count - 1) contents.Add(content);
			}

			_logger.LogDebug($"Done: {debugGeneratePostContent}");
			return contents;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var url = config.DiscordWebhookURL;
				var contents = GeneratePostContent(records);
				var client = new HttpClient();

				foreach (var content in contents) {
					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(NotifyConfig config, string asfResult) {
			try {
				_logger.LogDebug(debugSendMessageASF);

				var client = new HttpClient();
				var url = config.DiscordWebhookURL;
				var content = new DiscordPostContent() {
					Content = "ASF Result",
				};

				content.Embeds.Add(
					new Embed() {
						Description = asfResult,
						Color = 11282734
					}
				);

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, data);

				_logger.LogDebug($"Done: {debugSendMessageASF}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessageASF}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
