using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Web;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class Bark: INotifiable {
		private readonly ILogger<Bark> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Bark";
		private readonly string debugSendMessageASF = "Send ASF result to Bark";
		#endregion

		public Bark(ILogger<Bark> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
			try {
				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();
				var webGet = new HtmlWeb();
				var resp = new HtmlDocument();

				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					resp = await webGet.LoadFromWebAsync(
						new StringBuilder()
							.Append(url)
							.Append(NotifyFormatStrings.barkUrlTitle)
							.Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
							.Append(new StringBuilder().AppendFormat(NotifyFormatStrings.barkUrlArgs))
							.ToString()
					);
					_logger.LogDebug(resp.Text);
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
				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();

				var webGet = new HtmlWeb();
				var resp = new HtmlDocument();

				resp = await webGet.LoadFromWebAsync(
					new StringBuilder()
						.Append(url)
						.Append(NotifyFormatStrings.barkUrlASFTitle)
						.Append(HttpUtility.UrlEncode(asfRecord))
						.ToString()
				);
				_logger.LogDebug(resp.Text);

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
