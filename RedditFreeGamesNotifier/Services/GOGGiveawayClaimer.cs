using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Strings;

namespace RedditFreeGamesNotifier.Services {
	internal class GOGGiveawayClaimer: IDisposable {
		private readonly ILogger<GOGGiveawayClaimer> _logger;

		public GOGGiveawayClaimer(ILogger<GOGGiveawayClaimer> logger) {
			_logger = logger;
		}

		public async Task Claim(Config config, List<FreeGameRecord> records) {
			if (!config.EnableGOGAutoClaim) {
				_logger.LogInformation(GOGAutoClaimerStrings.infoDisabled);
				return;
			}

			if (records.Count == 0) {
				_logger.LogInformation(GOGAutoClaimerStrings.infoNoRecords);
				return;
			}

			try {
				_logger.LogDebug(GOGAutoClaimerStrings.debugClaim);

				var httpClient = new HttpClient();

				foreach (var record in records) {
					_logger.LogInformation(GOGAutoClaimerStrings.infoClaiming, record.Name);
					
					var request = new HttpRequestMessage() {
						Method = HttpMethod.Get,
						RequestUri = new Uri(GOGAutoClaimerStrings.Url),
						Headers = {
							{ GOGAutoClaimerStrings.UAKey, GOGAutoClaimerStrings.UAValue },
							{ GOGAutoClaimerStrings.CookieKey, config.Cookie }
						}
					};
					var resp = await httpClient.SendAsync(request);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {GOGAutoClaimerStrings.debugClaim}");
			} catch (Exception) {
				_logger.LogError($"Error: {GOGAutoClaimerStrings.debugClaim}");
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
