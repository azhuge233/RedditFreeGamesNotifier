using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;

namespace RedditFreeGamesNotifier.Services {
	internal class ConfigValidator(ILogger<ConfigValidator> logger, IOptions<Config> config) : IDisposable {
		private readonly ILogger<ConfigValidator> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugCheckValid = "Check config file validation";
		#endregion

		internal void CheckValid() {
			try {
				_logger.LogDebug(debugCheckValid);

				//Telegram
				if (config.EnableTelegram) {
					if (string.IsNullOrEmpty(config.TelegramToken))
						throw new Exception(message: "No Telegram Token provided!");
					if (string.IsNullOrEmpty(config.TelegramChatID))
						throw new Exception(message: "No Telegram ChatID provided!");
				}

				//Bark
				if (config.EnableBark) {
					if (string.IsNullOrEmpty(config.BarkAddress))
						throw new Exception(message: "No Bark Address provided!");
					if (string.IsNullOrEmpty(config.BarkToken))
						throw new Exception(message: "No Bark Token provided!");
				}

				//Email
				if (config.EnableEmail) {
					if (string.IsNullOrEmpty(config.FromEmailAddress))
						throw new Exception(message: "No from email address provided!");
					if (string.IsNullOrEmpty(config.ToEmailAddress))
						throw new Exception(message: "No to email address provided!");
					if (string.IsNullOrEmpty(config.SMTPServer))
						throw new Exception(message: "No SMTP server provided!");
					if (string.IsNullOrEmpty(config.AuthAccount))
						throw new Exception(message: "No email auth account provided!");
					if (string.IsNullOrEmpty(config.AuthPassword))
						throw new Exception(message: "No email auth password provided!");
				}

				//QQ Http
				if (config.EnableQQHttp) {
					if (string.IsNullOrEmpty(config.QQHttpAddress))
						throw new Exception(message: "No QQ http address provided!");
					if (string.IsNullOrEmpty(config.QQHttpPort.ToString()))
						throw new Exception(message: "No QQ http port provided!");
					if (string.IsNullOrEmpty(config.ToQQID))
						throw new Exception(message: "No QQ ID provided!");
					if (string.IsNullOrEmpty(config.QQHttpToken))
						_logger.LogInformation("No QQ Http token provided, make sure to set it right if token is enabled in your server settings.");
				}

				//QQ WebSocket
				if (config.EnableQQWebSocket) {
					if (string.IsNullOrEmpty(config.QQWebSocketAddress))
						throw new Exception(message: "No QQ WebSocket address provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketPort.ToString()))
						throw new Exception(message: "No QQ WebSocket port provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketToken))
						throw new Exception(message: "No QQ WebSocket token provided!");
					if (string.IsNullOrEmpty(config.ToQQID))
						throw new Exception(message: "No QQ ID provided!");
					if (string.IsNullOrEmpty(config.QQWebSocketToken))
						_logger.LogInformation("No QQ WebSocket token provided, make sure to set it right if token is enabled in your server settings.");
				}

				//PushPlus
				if (config.EnablePushPlus) {
					if (string.IsNullOrEmpty(config.PushPlusToken))
						throw new Exception(message: "No PushPlus token provided!");
				}

				//DingTalk
				if (config.EnableDingTalk) {
					if (string.IsNullOrEmpty(config.DingTalkBotToken))
						throw new Exception(message: "No DingTalk token provided!");
				}

				//PushDeer
				if (config.EnablePushDeer) {
					if (string.IsNullOrEmpty(config.PushDeerToken))
						throw new Exception(message: "No PushDeer token provided!");
				}

				//Discord
				if (config.EnableDiscord) {
					if (string.IsNullOrEmpty(config.DiscordWebhookURL))
						throw new Exception(message: "No Discord Webhook provided!");
				}

				// Meow
				if (config.EnableMeow) {
					if (string.IsNullOrEmpty(config.MeowAddress))
						throw new Exception(message: "No Meow Address provided!");
					if (string.IsNullOrEmpty(config.MeowNickname))
						throw new Exception(message: "No Meow Nickname provided!");
				}

				//ASF
				if (config.EnableASF) {
					if (string.IsNullOrEmpty(config.ASFIPCUrl))
						throw new Exception(message: "No ASF IPC Url provided!");
				}

				//GOG
				if (config.EnableGOGAutoClaim) {
					if (string.IsNullOrEmpty(config.Cookie)) {
						throw new Exception(message: "No GOG cookies provided!");
					}
				}

				_logger.LogDebug($"Done: {debugCheckValid}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugCheckValid}");
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
