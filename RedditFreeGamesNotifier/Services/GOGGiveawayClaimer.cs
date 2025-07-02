using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Strings;

namespace RedditFreeGamesNotifier.Services {
	internal class GOGGiveawayClaimer(ILogger<GOGGiveawayClaimer> logger, IOptions<Config> config) : IDisposable {
		private readonly ILogger<GOGGiveawayClaimer> _logger = logger;
		private readonly Config config = config.Value;

		public async Task Claim(bool HasGOGGiveaway) {
			if (!config.EnableGOGAutoClaim) {
				_logger.LogInformation(GOGAutoClaimerStrings.infoDisabled);
				return;
			}

			if (!HasGOGGiveaway) {
				_logger.LogInformation(GOGAutoClaimerStrings.infoNoRecords);
				return;
			}

			try {
				_logger.LogDebug(GOGAutoClaimerStrings.debugClaim);

				using var httpClient = new HttpClient();
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
