using System.Net;
using Microsoft.Extensions.Logging;
using RedditFreeGamesNotifier.Strings;

namespace RedditFreeGamesNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger) : IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal HttpClient RedditClient { get; set; }
		internal HttpClient OthersClient { get; set; } = new HttpClient();

		internal async Task<Dictionary<string, string>> GetSource() {
			try {
				_logger.LogDebug(ScrapeStrings.debugGetSource);
				var results = new Dictionary<string, string>();

				RedditClient = new HttpClient(
					new HttpClientHandler() {
						CookieContainer = CreateCookie()
					}
				);

				RedditClient.DefaultRequestHeaders.Add("User-Agent", ScrapeStrings.UAs[new Random().Next(0, ScrapeStrings.UAs.Length - 1)]);

				foreach (var url in ScrapeStrings.RedditUrls) {
					_logger.LogDebug(ScrapeStrings.debugGetSourceWithUrl, url);
					var response = await RedditClient.GetAsync(url);
					var content = await response.Content.ReadAsStringAsync();
					results.Add(url, content);

					// _logger.LogDebug($"response content: {content}");

					await Task.Delay(new Random().Next(500, 800));
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
				var response = await OthersClient.GetAsync(url);
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

		private static CookieContainer CreateCookie() { 
			var cookieContainer = new CookieContainer();

			var redditCookie = new Cookie() {
				Name = ScrapeStrings.RedditCookieName,
				Value = string.Empty,
				Domain = ScrapeStrings.RedditCookieDomain,
				Path = ScrapeStrings.RedditCookiePath,
				Expires = DateTime.Now.AddYears(1),
				Secure = true,
				HttpOnly = true
			};

			cookieContainer.Add(redditCookie);

			return cookieContainer;
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
