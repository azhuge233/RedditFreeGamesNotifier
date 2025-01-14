﻿using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal class TelegramBot: INotifiable {
		private readonly ILogger<TelegramBot> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Telegram";
		private readonly string debugSendMessageASF = "Send ASF result to Telegram";
		#endregion

		public TelegramBot(ILogger<TelegramBot> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<NotifyRecord> records) {
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

		public async Task SendMessage(NotifyConfig config, string asfResult) {
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
