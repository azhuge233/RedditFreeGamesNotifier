using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.ASFOP;
using RedditFreeGamesNotifier.Models.Record;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using RedditFreeGamesNotifier.Strings;
using RedditFreeGamesNotifier.Models.Config;

namespace RedditFreeGamesNotifier.Services {
	internal class ASFOP : IDisposable {
		private readonly ILogger<ASFOP> _logger;

		#region debug strings
		private readonly string debugASFOP = "ASFOP";
		private readonly string debugGenerateSubIDString = "GenerateSubIDString";
		private readonly string infoAddlicenseResult = "Addlicense result: \n";
		private readonly string infoNoRecords = "No new record, skipping addlicense";
		private readonly string infoASFDisabled = "ASF disabled, skipping";
		#endregion

		public ASFOP(ILogger<ASFOP> logger) {
			_logger = logger;
		}

		private string GenerateSubIDString(List<FreeGameRecord> gameList) {
			try {
				_logger.LogDebug(debugGenerateSubIDString);

				var sb = new StringBuilder();
				gameList.ForEach(game => sb.Append(sb.Length == 0 ? game.AppId : $",{game.AppId}"));

				_logger.LogDebug($"Done: {debugGenerateSubIDString}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {debugGenerateSubIDString}");
				throw;
			}
		}

		internal async Task<string> Addlicense(Config config, List<FreeGameRecord> gameList) {
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

				var url = new StringBuilder().AppendFormat(ASFStrings.commandUrl, config.ASFIPCUrl).ToString();
				var content = new StringContent(JsonSerializer.Serialize(new AddlicenssPostContent() { Command = $"{ASFStrings.addlicenseCommand}{GenerateSubIDString(gameList)}" }), Encoding.UTF8, "application/json");

				var response = await client.PostAsync(url, content);
				_logger.LogDebug(response.ToString());
				var responseContent = JsonSerializer.Deserialize<ASFResponseContent>(await response.Content.ReadAsStringAsync()).Result;
				_logger.LogInformation($"{infoAddlicenseResult}{responseContent}");

				_logger.LogDebug($"Done: {debugASFOP}");
				return responseContent;
			} catch (Exception) {
				_logger.LogError($"Error: {debugASFOP}");
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
