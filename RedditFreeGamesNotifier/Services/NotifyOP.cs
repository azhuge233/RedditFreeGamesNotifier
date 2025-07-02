using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Services.Notifier;

namespace RedditFreeGamesNotifier.Services {
	internal class NotifyOP(ILogger<NotifyOP> logger, IOptions<Config> config, TelegramBot tgBot, Bark bark, QQHttp qqHttp, QQWebSocket qqWS, PushPlus pushPlus, DingTalk dingTalk, PushDeer pushDeer, Discord discord, Email email, Meow meow) : IDisposable {
		private readonly ILogger<NotifyOP> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugNotify = "Notify";
		private readonly string debugEnabledFormat = "Sending notifications to {0}";
		private readonly string debugDisabledFormat = "{0} notify is disabled, skipping";
		private readonly string debugNoNewNotifications = "No new notifications! Skipping";
		private readonly string debugGenerateNotifyRecordList = "Generating notify record";
		#endregion

		internal async Task Notify(List<FreeGameRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(debugNoNewNotifications);
				return;
			}

			var pushListFinal = GenerateNotifyRecordList(pushList);

			try {
				_logger.LogDebug(debugNotify);

				var notifyTasks = new List<Task>();

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(debugEnabledFormat, "Telegram");
					notifyTasks.Add(tgBot.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(debugEnabledFormat, "Bark");
					notifyTasks.Add(bark.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "Bark");

				// QQ Http notifications
				if (config.EnableQQHttp) {
					_logger.LogInformation(debugEnabledFormat, "QQ Http");
					notifyTasks.Add(qqHttp.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "QQ Http");

				// QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
					_logger.LogInformation(debugEnabledFormat, "QQ WebSocket");
					notifyTasks.Add(qqWS.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "QQ WebSocket");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(debugEnabledFormat, "PushPlus");
					notifyTasks.Add(pushPlus.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(debugEnabledFormat, "DingTalk");
					notifyTasks.Add(dingTalk.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(debugEnabledFormat, "PushDeer");
					notifyTasks.Add(pushDeer.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(debugEnabledFormat, "Discord");
					notifyTasks.Add(discord.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "Discord");

				// Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(debugEnabledFormat, "Email");
					notifyTasks.Add(email.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "Email");

				// Meow notifications
				if (config.EnableMeow) {
					_logger.LogInformation(debugEnabledFormat, "Meow");
					notifyTasks.Add(meow.SendMessage(pushListFinal));
				} else _logger.LogInformation(debugDisabledFormat, "Meow");

				await Task.WhenAll(notifyTasks);

				_logger.LogDebug($"Done: {debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugNotify}");
				throw;
			} finally {
				//Dispose();
			}
		}

		internal async Task Notify(string asfResult) {
			if (string.IsNullOrEmpty(asfResult)) {
				_logger.LogInformation(debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(debugNotify);

				var notifyTasks = new List<Task>();

				if (config.NotifyASFResult) {
					// Telegram notifications
					if (config.EnableTelegram) {
						_logger.LogInformation(debugEnabledFormat, "Telegram");
						notifyTasks.Add(tgBot.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "Telegram");

					// Bark notifications
					if (config.EnableBark) {
						_logger.LogInformation(debugEnabledFormat, "Bark");
						notifyTasks.Add(bark.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "Bark");

					// QQ Http notifications
					if (config.EnableQQHttp) {
						_logger.LogInformation(debugEnabledFormat, "QQ Http");
						notifyTasks.Add(qqHttp.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "QQ Http");

					// QQ WebSocket notifications
					if (config.EnableQQWebSocket) {
						_logger.LogInformation(debugEnabledFormat, "QQ WebSocket");
						notifyTasks.Add(qqWS.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "QQ WebSocket");

					// PushPlus notifications
					if (config.EnablePushPlus) {
						_logger.LogInformation(debugEnabledFormat, "PushPlus");
						notifyTasks.Add(pushPlus.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "PushPlus");

					// DingTalk notifications
					if (config.EnableDingTalk) {
						_logger.LogInformation(debugEnabledFormat, "DingTalk");
						notifyTasks.Add(dingTalk.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "DingTalk");

					// PushDeer notifications
					if (config.EnablePushDeer) {
						_logger.LogInformation(debugEnabledFormat, "PushDeer");
						notifyTasks.Add(pushDeer.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "PushDeer");

					// Discord notifications
					if (config.EnableDiscord) {
						_logger.LogInformation(debugEnabledFormat, "Discord");
						notifyTasks.Add(discord.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "Discord");

					// Email notifications
					if (config.EnableEmail) {
						_logger.LogInformation(debugEnabledFormat, "Email");
						notifyTasks.Add(email.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "Email");

					// Meow notifications
					if (config.EnableMeow) {
						_logger.LogInformation(debugEnabledFormat, "Meow");
						notifyTasks.Add(meow.SendMessage(asfResult));
					} else _logger.LogInformation(debugDisabledFormat, "Meow");

					await Task.WhenAll(notifyTasks);
				} else _logger.LogDebug(debugDisabledFormat, "ASF");

				_logger.LogDebug($"Done: {debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugNotify}");
				throw;
			} finally {
				Dispose();
			}
		}

		private List<NotifyRecord> GenerateNotifyRecordList(List<FreeGameRecord> pushList) {
			_logger.LogDebug(debugGenerateNotifyRecordList);
			var resultList = new List<NotifyRecord>();

			try {
				resultList = [.. pushList.Select(record => new NotifyRecord(record))];
			} catch (Exception) {
				_logger.LogError($"Error: {debugGenerateNotifyRecordList}");
				throw;
			}

			_logger.LogDebug($"Done: {debugGenerateNotifyRecordList}");
			return resultList;
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
