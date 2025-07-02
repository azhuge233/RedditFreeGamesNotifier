using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.PostContent;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class PushPlus(ILogger<PushPlus> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<PushPlus> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to PushPlus";
		private readonly string debugCreateMessage = "Create notification message";
		private readonly string debugSendMessageASF = "Send ASF result to PushPlus";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var client = new HttpClient();

				var title = new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusTitleFormat, records.Count).ToString();

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = CreateMessage(records)
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(debugSendMessageASF);

				var client = new HttpClient();

				var title = NotifyFormatStrings.pushPlusASFTitleFormat;

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = asfResult
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {debugSendMessageASF}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessageASF}");
				throw;
			} finally {
				Dispose();
			}
		}

		private string CreateMessage(List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugCreateMessage);

				var sb = new StringBuilder();

				records.ForEach(record => sb.AppendFormat(NotifyFormatStrings.pushPlusBodyFormat, record.ToPushPlusMessage()));

				sb.Append(NotifyFormatStrings.projectLinkHTML);

				_logger.LogDebug($"Done: {debugCreateMessage}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {debugCreateMessage}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
