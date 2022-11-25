using Microsoft.Extensions.Logging;
using System.Text.Json;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.PostContent;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class DingTalk: INotifiable {
		private readonly ILogger<DingTalk> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notifications to DingTalk";
		#endregion

		public DingTalk(ILogger<DingTalk> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var url = new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken).ToString();
				var content = new DingTalkPostContent();

				var client = new HttpClient();
				var data = new StringContent("");
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					content.Text.Content_ = $"{record.ToDingTalkMessage()}{NotifyFormatStrings.projectLink}";
					data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					resp = await client.PostAsync(url, data);
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
				_logger.LogDebug(debugSendMessage);

				var url = new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken).ToString();
				var content = new DingTalkPostContent();

				var client = new HttpClient();
				var data = new StringContent("");
				var resp = new HttpResponseMessage();

				content.Text.Content_ = $"{asfResult}";
				data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				resp = await client.PostAsync(url, data);
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
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
