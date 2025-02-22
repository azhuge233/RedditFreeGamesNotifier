using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.PostContent;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class Meow: INotifiable {
		private readonly ILogger<Meow> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Meow";
		private readonly string debugSendMessageASF = "Send ASF result to Meow";
		#endregion

		public Meow(ILogger<Meow> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname).ToString();

				var content = new MeowPostContent();

				var client = new HttpClient();
				var data = new StringContent("");
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					content.Title = NotifyFormatStrings.meowUrlTitle;
					content.Message= $"{record.ToMeowMessage()}{NotifyFormatStrings.projectLink}";
					content.Url = record.Url;

					data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					resp = await client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					await Task.Delay(3000); // Meow has a rate limit of 1 request per 3 seconds
				}
				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(NotifyConfig config, string asfRecord) {
			try {
				_logger.LogDebug(debugSendMessageASF);
				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname).ToString();

				var content = new MeowPostContent();

				var client = new HttpClient();
				var data = new StringContent("");
				var resp = new HttpResponseMessage();

				content.Title = "ASF result";
				content.Message = $"{asfRecord}";
				data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

				resp = await client.PostAsync(url, data);
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {debugSendMessageASF}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessageASF}");
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
