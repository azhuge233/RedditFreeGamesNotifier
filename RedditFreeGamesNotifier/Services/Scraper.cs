using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Strings;

namespace RedditFreeGamesNotifier.Services {
	internal class Scraper: IDisposable {
		private readonly ILogger<Scraper> _logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		public Scraper(ILogger<Scraper> logger) {
			_logger = logger;
		}

		internal async Task<Dictionary<string, string>> GetSource() {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSource);
				var results = new Dictionary<string, string>();

				foreach (var url in ScrapeStrings.RedditUrls) {
					_logger.LogDebug(ScrapeStrings.debugGetSourceWithUrl, url);
					var response = await Client.GetAsync(url);
					results.Add(url, await response.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSource}");
				return results;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
				throw;
			} finally { 
				Dispose();
			}
		}

		internal async Task<string> GetSource(string url) {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSource);

				_logger.LogDebug(ScrapeStrings.debugGetSourceWithUrl, url);
				var response = await Client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSource}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
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
