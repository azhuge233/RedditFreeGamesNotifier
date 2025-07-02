using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class TelegramBot(ILogger<TelegramBot> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<TelegramBot> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Telegram";
		private readonly string debugSendMessageASF = "Send ASF result to Telegram";
		#endregion

		public async Task SendMessage(List<NotifyRecord> records) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				foreach (var record in records) {
					_logger.LogDebug($"{debugSendMessage} : {record.Name}");
					await BotClient.SendMessage(
						chatId: config.TelegramChatID,
						text: $"{record.ToTelegramMessage()}{NotifyFormatStrings.projectLinkHTML.Replace("<br>", "\n")}",
						parseMode: ParseMode.Html
					);
				}

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(string asfResult) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				_logger.LogDebug(debugSendMessageASF);
				await BotClient.SendMessage(
					chatId: config.TelegramChatID,
					text: $"{asfResult.Replace("<", "&lt;").Replace(">", "&gt;")}",
					parseMode: ParseMode.Html
				);

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
