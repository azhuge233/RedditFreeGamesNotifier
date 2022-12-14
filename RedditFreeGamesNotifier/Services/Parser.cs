using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using RedditFreeGamesNotifier.Models;
using RedditFreeGamesNotifier.Models.Record;
using RedditFreeGamesNotifier.Modules;
using RedditFreeGamesNotifier.Strings;
using System.Text;
using System.Text.RegularExpressions;
using RedditFreeGamesNotifier.Models.SteamApi;

namespace RedditFreeGamesNotifier.Services {
	internal class Parser: IDisposable {
		private readonly ILogger<Parser> _logger;
		private readonly IServiceProvider services = DI.BuildDiScraperOnly();

		public Parser(ILogger<Parser> logger) {
			_logger = logger;
		}

		internal async Task<ParseResult> Parse(Dictionary<string, string> source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseStrings.debugParse);
				var result = new ParseResult();

				foreach(var pair in source) {
					_logger.LogDebug(ParseStrings.debugParseWithUrl, pair.Key);
					var htmlDoc = new HtmlDocument();
					htmlDoc.LoadHtml(pair.Value);

					var divs = htmlDoc.DocumentNode.SelectNodes(ParseStrings.redditDivXPath).ToList();

					foreach (var div in divs) {
						var spans = div.SelectNodes(ParseStrings.redditEndedPXPath);
						var spanInnerText = spans.Count > 1 ? div.SelectSingleNode(ParseStrings.redditEndedSpanXPath).InnerText.ToLower() : string.Empty;
						var dataDomain = div.Attributes[ParseStrings.dataDomainKey].Value;
						if (ParseStrings.ignoreKeywords.Contains(spanInnerText) || !ParseStrings.SupportedPlatform.Keys.Any(dataDomain.EndsWith)) continue;

						if (dataDomain.EndsWith("itch.io")) dataDomain = "itch.io";

						var dataUrl = div.Attributes[ParseStrings.dataUrlKey].Value.TrimEnd('/').Split('?').First();
						var dataPermaLink = div.Attributes[ParseStrings.dataRedditPermaLinkKey].Value;
						var redditTitle = div.SelectSingleNode(ParseStrings.redditTitleLinkXPath).InnerText;

						var platform = ParseStrings.SupportedPlatform[dataDomain];
						var redditLink = new StringBuilder().Append(ParseStrings.redditUrl).Append(dataPermaLink).ToString();
						var appId = GetGameId(dataUrl);

						if (result.Records.Any(record => record.Url == dataUrl)) {
							_logger.LogDebug(ParseStrings.debugFoundInPreviousPage, dataUrl);
							continue;
						}
						if (platform == "Steam" && appId == string.Empty) {
							_logger.LogDebug(ParseStrings.debugSteamIDNotDetected, dataUrl);
							continue;
						}
						if (platform == "Itch.io" && !await IsClaimable(dataUrl)) {
							_logger.LogDebug(ParseStrings.debugItchIOCNotClaimable, dataUrl);
							continue;
						}

						_logger.LogDebug($"{dataUrl} | {redditLink} | {platform} | {appId}");

						var newRecord = new FreeGameRecord() { 
							Url = dataUrl,
							RedditUrl = redditLink,
							Platform = platform,
							AppId = appId
						};
						newRecord.Name = await GetGameName(newRecord, redditTitle);

						result.Records.Add(newRecord);

						if (!oldRecords.Any(record => record.RedditUrl == newRecord.RedditUrl || record.Url == newRecord.Url)) {
							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Name);

							if (!string.IsNullOrEmpty(newRecord.AppId)) result.SteamFreeGames.Add(newRecord);
							else if (newRecord.Platform == "GOG") result.GOGGiveawayRecords.Add(newRecord);

							result.NotifyRecords.Add(newRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, newRecord.Name);
					}
					_logger.LogDebug($"{ParseStrings.debugParseWithUrl}", pair.Key);
				}

				_logger.LogDebug($"Done: {ParseStrings.debugParse}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParse}");
				throw;
			} finally {
				Dispose();
			}
		}

		private static string GetGameId(string url) {
			var appIdMatch = Regex.Match(url, ParseStrings.appIdRegex);
			var subIdMatch = Regex.Match(url, ParseStrings.subIdRegex);
			return appIdMatch.Success ? appIdMatch.Value : subIdMatch.Success ? subIdMatch.Value : string.Empty;
		}

		private async Task<string> GetGameName(FreeGameRecord record, string redditTitle) {
			try {
				var gameName = redditTitle;

				if (record.Platform == "GOG") {
					_logger.LogDebug(ParseStrings.debugGetGameNameWithUrl, record.Url);

					if (!record.Url.Contains(ParseStrings.gogGiveawayUrlKeyword)) {
						var source = await services.GetRequiredService<Scraper>().GetSource(record.Url);
						var htmlDoc = new HtmlDocument();
						htmlDoc.LoadHtml(source);

						// Fixes some bad links skip to GOG's all games page, causes fetch game name error
						// If the page title ends with "DRM-free | GOG.COM" means the link is bad, return reddit Title instead
						var gogTitle = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.gogTitleXPath).InnerText;
						if (!gogTitle.Contains(ParseStrings.gogAllGamesPageTitle))
							gameName = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.gogGameTitleXPath).InnerText;
					} else _logger.LogDebug(ParseStrings.debugIsGOGGiveaway, record.Url);
				}

				if (record.Platform == "Steam") {
					var appId = record.AppId.Split('/')[1];
					var appDetailsUrl = ParseStrings.steamApiurlPrefix + appId;
					_logger.LogDebug(ParseStrings.debugGetGameNameWithUrl, appDetailsUrl);

					var source = await services.GetRequiredService<Scraper>().GetSource(appDetailsUrl);
					var json = JsonSerializer.Deserialize<Dictionary<string, AppDetails>>(source);

					gameName = json[appId].Data[ParseStrings.steamAppDetailGameNameKey].ToString();
				}

				_logger.LogDebug($"Done: {ParseStrings.debugGetGameNameWithUrl}", record.Url);
				return gameName;
			} catch (Exception) {
				_logger.LogError(ParseStrings.errorGetGameName, record.Url);

				return redditTitle;
			}
		}

		private async Task<bool> IsClaimable(string url) {
			var result = true;

			try {
				_logger.LogDebug($"{ParseStrings.debugCheckItchIOClaimable} | {url}");

				var source = await services.GetRequiredService<Scraper>().GetSource(url);
				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(source);

				var buyButton = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.itchioBuyButtonXPath);
				// var downloadButton = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.itchioDownloadButtonXPath);

				//result = buyButton != null && downloadButton == null && buyButton.InnerText.Contains(ParseStrings.itchioDownloadOrClaimText);
				result = buyButton != null && buyButton.InnerText.Contains(ParseStrings.itchioDownloadOrClaimText);

				_logger.LogDebug($"Done: {ParseStrings.debugCheckItchIOClaimable}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugCheckItchIOClaimable}");

				return result;
			}
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
