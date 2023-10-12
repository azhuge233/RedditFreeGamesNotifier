using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Modules;
using RedditFreeGamesNotifier.Services.Notifier;
using Telegram.Bot.Types;

namespace RedditFreeGamesNotifier.Services {
	internal class NotifyOP : IDisposable {
		private readonly ILogger<NotifyOP> _logger;
		private readonly IServiceProvider services = DI.BuildDiNotifierOnly();

		#region debug strings
		private readonly string debugNotify = "Notify";
		private readonly string debugEnabledFormat = "Sending notifications to {0}";
		private readonly string debugDisabledFormat = "{0} notify is disabled, skipping";
		private readonly string debugNoNewNotifications = "No new notifications! Skipping";
		private readonly string debugGenerateNotifyRecordList = "Generating notify record";
		#endregion

		public NotifyOP(ILogger<NotifyOP> logger) {
			_logger = logger;
		}

		private List<NotifyRecord> GenerateNotifyRecordList(List<FreeGameRecord> pushList) {
			_logger.LogDebug(debugGenerateNotifyRecordList);
			var resultList = new List<NotifyRecord>();

			try {
				resultList = pushList.Select(record => new NotifyRecord(record)).ToList();
			} catch (Exception) {
				_logger.LogError($"Error: {debugGenerateNotifyRecordList}");
				throw;
			}

			_logger.LogDebug($"Done: {debugGenerateNotifyRecordList}");
			return resultList;
		}

		internal async Task Notify(NotifyConfig config, List<FreeGameRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(debugNoNewNotifications);
				return;
			}

			var pushListFinal = GenerateNotifyRecordList(pushList);

			try {
				_logger.LogDebug(debugNotify);

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(debugEnabledFormat, "Telegram");
					await services.GetRequiredService<TelegramBot>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(debugEnabledFormat, "Bark");
					await services.GetRequiredService<Bark>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "Bark");

				// QQ notifications
				if (config.EnableQQ) {
					_logger.LogInformation(debugEnabledFormat, "QQ");
					await services.GetRequiredService<QQ>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "QQ");

				//QQ Red (Chronocat) notifications
				if (config.EnableRed) {
					_logger.LogInformation(debugEnabledFormat, "QQ Red (Chronocat)");
					await services.GetRequiredService<QQRed>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "QQ Red (Chronocat)");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(debugEnabledFormat, "PushPlus");
					await services.GetRequiredService<PushPlus>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(debugEnabledFormat, "DingTalk");
					await services.GetRequiredService<DingTalk>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(debugEnabledFormat, "PushDeer");
					await services.GetRequiredService<PushDeer>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(debugEnabledFormat, "Discord");
					await services.GetRequiredService<Discord>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "Discord");

				// Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(debugEnabledFormat, "Email");
					await services.GetRequiredService<Email>().SendMessage(config, pushListFinal);
				} else _logger.LogInformation(debugDisabledFormat, "Email");

				_logger.LogDebug($"Done: {debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugNotify}");
				throw;
			} finally {
				//Dispose();
			}
		}

		internal async Task Notify(ASFConfig config, string asfResult) {
			if (string.IsNullOrEmpty(asfResult)) {
				_logger.LogInformation(debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(debugNotify);

				if (config.NotifyASFResult) {
					// Telegram notifications
					if (config.EnableTelegram) {
						_logger.LogInformation(debugEnabledFormat, "Telegram");
						await services.GetRequiredService<TelegramBot>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "Telegram");

					// Bark notifications
					if (config.EnableBark) {
						_logger.LogInformation(debugEnabledFormat, "Bark");
						await services.GetRequiredService<Bark>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "Bark");

					// QQ notifications
					if (config.EnableQQ) {
						_logger.LogInformation(debugEnabledFormat, "QQ");
						await services.GetRequiredService<QQ>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "QQ");

					// PushPlus notifications
					if (config.EnablePushPlus) {
						_logger.LogInformation(debugEnabledFormat, "PushPlus");
						await services.GetRequiredService<PushPlus>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "PushPlus");

					// DingTalk notifications
					if (config.EnableDingTalk) {
						_logger.LogInformation(debugEnabledFormat, "DingTalk");
						await services.GetRequiredService<DingTalk>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "DingTalk");

					// PushDeer notifications
					if (config.EnablePushDeer) {
						_logger.LogInformation(debugEnabledFormat, "PushDeer");
						await services.GetRequiredService<PushDeer>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "PushDeer");

					// Discord notifications
					if (config.EnableDiscord) {
						_logger.LogInformation(debugEnabledFormat, "Discord");
						await services.GetRequiredService<Discord>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "Discord");

					// Email notifications
					if (config.EnableEmail) {
						_logger.LogInformation(debugEnabledFormat, "Email");
						await services.GetRequiredService<Email>().SendMessage(config, asfResult);
					} else _logger.LogInformation(debugDisabledFormat, "Email");
				} else _logger.LogDebug(debugDisabledFormat, "ASF");

				_logger.LogDebug($"Done: {debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugNotify}");
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
