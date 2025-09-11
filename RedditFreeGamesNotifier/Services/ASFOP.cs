using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.ASFOP;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;

namespace RedditFreeGamesNotifier.Services {
	internal class ASFOP(ILogger<ASFOP> logger, IOptions<Config> config) : IDisposable {
		private readonly ILogger<ASFOP> _logger = logger;
		private readonly Config config = config.Value;

		#region debug strings
		private readonly string debugASFOP = "ASFOP";
		private readonly string debugGenerateSubIDString = "Generate Commands";
		private readonly string infoAddlicenseResult = "Claim result: \n";
		private readonly string infoNoRecords = "No new record, skipping auto claim";
		private readonly string infoASFDisabled = "ASF disabled, skipping";
		#endregion

		internal async Task<string> Claim(List<FreeGameRecord> gameList) {
			if (!config.EnableASF) {
				_logger.LogInformation(infoASFDisabled);
				return string.Empty;
			}

			if (gameList.Count == 0) {
				_logger.LogInformation(infoNoRecords);
				return string.Empty;
			}

			try {
				_logger.LogDebug(debugASFOP);

				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authentication", config.ASFIPCPassword);
				client.DefaultRequestHeaders.Add(ASFStrings.UAKey, ASFStrings.UAValue);

				var url = string.Format(ASFStrings.commandUrl, config.ASFIPCUrl);

				var commands = GenerateCommands(gameList);

				var responseContents = new List<string>();
				foreach (var command in commands) {
					var postContent = new AutoClaimPostContent() { Command = command };
					var serializedContent = new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json");

					var response = await client.PostAsync(url, serializedContent);
					_logger.LogDebug(response.ToString());

					var asfResponse = await response.Content.ReadAsStringAsync();
					var responseContent = JsonSerializer.Deserialize<ASFResponseContent>(asfResponse).Result;
					_logger.LogInformation($"{infoAddlicenseResult}{responseContent}");

					responseContents.Add(responseContent);
				}

				_logger.LogDebug($"Done: {debugASFOP}");
				return string.Join($"{Environment.NewLine}{Environment.NewLine}", responseContents);
			} catch (Exception) {
				_logger.LogError($"Error: {debugASFOP}");
				throw;
			} finally {
				Dispose();
			}
		}

		private List<string> GenerateCommands(List<FreeGameRecord> gameList) {
			try {
				_logger.LogDebug(debugGenerateSubIDString);

				List<string> commands = [];

				var groups = gameList.GroupBy(game => game.IsSteamFest);

				foreach (var group in groups) {
					var commandPrefix = group.Key ? ASFStrings.redeemPointCommand : ASFStrings.addlicenseCommand;
					commands.Add($"{commandPrefix}{string.Join(',', group.Select(item => item.AppId))}");
					_logger.LogDebug(commands[^1]);
				}

				_logger.LogDebug($"Done: {debugGenerateSubIDString}");
				return commands;
			} catch (Exception) {
				_logger.LogError($"Error: {debugGenerateSubIDString}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
