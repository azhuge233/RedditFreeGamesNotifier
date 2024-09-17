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
						#region get game info
						var spans = div.SelectNodes(ParseStrings.redditEndedPXPath);
						var spanInnerText = spans.Count > 1 ? div.SelectSingleNode(ParseStrings.redditEndedSpanXPath).InnerText.ToLower() : string.Empty;
						var dataDomain = div.Attributes[ParseStrings.dataDomainKey].Value;

						// Check if post is in supported game platform list
						if (ParseStrings.ignoreKeywords.Contains(spanInnerText) || !ParseStrings.SupportedPlatform.Keys.Any(dataDomain.EndsWith)) continue;

						if (dataDomain.EndsWith("itch.io")) dataDomain = "itch.io";

						var dataUrl = div.Attributes[ParseStrings.dataUrlKey].Value.TrimEnd('/').Split('?').First();
						var dataPermaLink = div.Attributes[ParseStrings.dataRedditPermaLinkKey].Value;
						var redditTitle = div.SelectSingleNode(ParseStrings.redditTitleLinkXPath).InnerText;

						var platform = ParseStrings.SupportedPlatform[dataDomain];
						var redditLink = new StringBuilder().Append(ParseStrings.redditUrl).Append(dataPermaLink).ToString();

						var isSteamPointsShopItem = platform == "Steam" && dataUrl.StartsWith(ParseStrings.steamPointsShopUrlPrefix);
						var isGOGGiveaway = platform == "GOG" && IsGOGGiveaway(dataUrl);

						var appId = isSteamPointsShopItem ? ParseStrings.steamPointsShopItem : GetGameId(dataUrl);
						var subId = string.Empty;
						AppDetails steamAppDetails = null;
						#endregion

						#region check free game duplication in other sources, old records must NOT included
						// Skip if found in other subreddits
						if (result.Records.Any(record => record.Url == dataUrl)) {
							_logger.LogDebug(ParseStrings.debugFoundInPreviousPage, dataUrl);
							continue;
						}

						///Platform sepicific
						// Steam
						if (platform == "Steam") {
							// no app/sub id
							if (appId == string.Empty) {
								_logger.LogDebug(ParseStrings.debugSteamIDNotDetected, dataUrl);
								continue;
							} else if (appId == ParseStrings.steamPointsShopItem) {
								_logger.LogDebug(ParseStrings.debugSteamPointsShtopItemDetected, dataUrl);
							} else if (result.Records.Any(record => record.AppId.Split(',').Contains(appId))) { 
								_logger.LogDebug(ParseStrings.debugSteamIDDuplicationDetected, dataUrl);
								continue;
							}
						}

						// GOG
						if (platform == "GOG") {
							if (result.Records.Any(record => record.IsGOGGiveaway == true) && isGOGGiveaway) {
								_logger.LogDebug(ParseStrings.debugGOGGiveawayDuplication, dataUrl);
								continue;
							}

							// fix some gog links with store region word (like 'en') but points to the same game
							if (result.Records.Any(record => record.Url.EndsWith(dataUrl.Split('/').Last()))) {
								_logger.LogDebug(ParseStrings.debugGOGDuplication, dataUrl);
								continue;
							}
						}
						#endregion

						#region extra validation and information gathering
						//Itchio
						if (platform == "Itch.io") {
							/// Solve notify duplication caused by network failing
							/*	The Reason
							 *	When network fails in 'IsClaimable()', 'IsClaimable()' will always return 'false'
							 *	When a previously checked claimable game meets a failing network, this game will be removed from records
							 *	So in the next successful run, the same game will be notified again 
							*/
							/*	The Solution
							 *	Check old records first
							 *	If it appears in old records, means that this current game is previously checked as a claimable game
							 *	Therefore there's no need to check this 'previously checked claimable game' is claimable or not
							 *	A claimable game will not calling 'IsClaimable()', so there will be no network failing, no notify duplication
							*/
							var isClaimable = oldRecords.Any(record => record.Url == dataUrl) || await IsClaimable(dataUrl);

							if (!isClaimable) {
								_logger.LogDebug(ParseStrings.debugItchIOCNotClaimable, dataUrl);
								continue;
							}
						}

						if (platform == "Steam" && !isSteamPointsShopItem) {
							steamAppDetails = await GetSteamAppDetails(appId.Split('/')[1]);
							subId = await GetSteamSubID(steamAppDetails);
						}
						#endregion

						_logger.LogDebug($"{dataUrl} | {redditLink} | {platform} | {appId}\n");

						var newRecord = new FreeGameRecord() {
							Url = dataUrl,
							RedditUrl = redditLink,
							Platform = platform,
							AppId = string.IsNullOrEmpty(subId) ? appId : $"{appId},{subId}",
							IsGOGGiveaway = isGOGGiveaway
						};
						newRecord.Name = await GetGameName(newRecord, steamAppDetails, redditTitle);

						result.Records.Add(newRecord);

						#region notification list
						if (!oldRecords.Any(record => record.RedditUrl == newRecord.RedditUrl || 
							record.Url == newRecord.Url || 
							(newRecord.Platform == "Steam" && !isSteamPointsShopItem && record.AppId == newRecord.AppId) )) {

							_logger.LogInformation(ParseStrings.infoFoundNewGame, newRecord.Name);

							if (newRecord.Platform == "Steam" && !string.IsNullOrEmpty(newRecord.AppId)) result.SteamFreeGames.Add(newRecord);
							else if (newRecord.Platform == "GOG" && newRecord.IsGOGGiveaway) result.HasGOGGiveaway = true;

							result.NotifyRecords.Add(newRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInOldRecords, newRecord.Name);
						#endregion
					}
					_logger.LogDebug($"Done: {ParseStrings.debugParseWithUrl}", pair.Key);
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

		private async Task<AppDetails> GetSteamAppDetails(string appId) {
			try {
				var appDetailsUrl = ParseStrings.steamApiAppDetailsPrefix + appId;
				_logger.LogDebug(ParseStrings.debugGetSteamAppDetails, appDetailsUrl);

				var source = await services.GetRequiredService<Scraper>().GetSource(appDetailsUrl);
				if(source == null || source == "null") return null;

				var json = JsonSerializer.Deserialize<Dictionary<string, AppDetails>>(source);

				_logger.LogDebug(ParseStrings.debugSteamApiFailed, appId);
				return json[appId];
			} catch (Exception ex) {
				_logger.LogDebug(ParseStrings.errorGetSteamAppDetails, appId);
				_logger.LogDebug(ex.Message);
				return null;
			}
		}

		private async Task<string> GetGameName(FreeGameRecord record, AppDetails appDetails, string redditTitle) {
			try {
				var gameName = redditTitle;

				if (record.Platform == "GOG") {
					_logger.LogDebug(ParseStrings.debugGetGameNameWithUrl, record.Url);

					if (!record.IsGOGGiveaway) {
						/// When url is GOG free partner page: https://www.gog.com/partner/free_games
						/// return reddit title
						/// skip fetching page source, since that will trigger unnecessary errors
						if (!record.Url.StartsWith(ParseStrings.gogFreePartnerUrl)) {
							var source = await services.GetRequiredService<Scraper>().GetSource(record.Url);
							var htmlDoc = new HtmlDocument();
							htmlDoc.LoadHtml(source);

							/// Fixes some gog redirection links, causes fetch game name error:
							///		- If the page title contains "DRM-free | GOG.COM" means the link is redirected to all game page
							///		- If the page title contains "GOG.COM | GOG.COM" means the link is redirected to GOG's main page
							/// Under above circumstances, return reddit Title instead
							var gogTitle = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.gogTitleXPath).InnerText;
							_logger.LogDebug($"GOG Title: {gogTitle}");

							if (!gogTitle.Contains(ParseStrings.gogAllGamesPageTitle) &&
								!gogTitle.Contains(ParseStrings.gogRedirectedToMainPageTitle))
								gameName = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.gogGameTitleXPath).InnerText.Trim();
						} else _logger.LogDebug(ParseStrings.debugGOGFreePartnerPage, redditTitle);
					} else _logger.LogDebug(ParseStrings.debugIsGOGGiveaway, record.Url);
				}

				if (record.Platform == "Steam") {
					if (appDetails != null && appDetails.Success) gameName = appDetails.Data.Name;
					else _logger.LogDebug(ParseStrings.debugGetGameNameAppDetailsFailed, record.AppId);
				}

				_logger.LogDebug($"Done: {ParseStrings.debugGetGameNameWithUrl}", record.Url);
				return gameName;
			} catch (Exception) {
				_logger.LogError(ParseStrings.errorGetGameName, record.Url);

				return redditTitle;
			}
		}

		private async Task<string> GetSteamSubID(AppDetails appDetails) {
			try {
				_logger.LogDebug(ParseStrings.debugGetSteamSubID);
				string freeSubsIDString = string.Empty;

				if (appDetails == null || !appDetails.Success) {
					_logger.LogDebug(ParseStrings.debugGetSteamSubIDAppDetailsFailed);
					return freeSubsIDString;
				}

				var data = appDetails.Data;

				if (data.Type == "dlc") {
					if (data.PackageGroups != null && data.PackageGroups.Count > 0) {
						var defaultPackageGroup = appDetails.Data.PackageGroups.First(pg => pg.Name == ParseStrings.steamAppDetailsGameTypeValueDefault);
						var freeSubs = defaultPackageGroup.Subs.Where(sub => sub.IsFreeLicense == true).ToList();
						freeSubsIDString = string.Join(",", freeSubs.Select(sub => $"{ParseStrings.subIdPrefix}{sub.PackageID}"));
						_logger.LogDebug(ParseStrings.debugGotSteamSubID, freeSubsIDString);
					}

					_logger.LogDebug(ParseStrings.debugGetSteamSubIDMainGameAppID);
					var fullGameAppDetails = await GetSteamAppDetails(data.FullGame.AppID);
					if (fullGameAppDetails != null && fullGameAppDetails.Success) {
						if (fullGameAppDetails.Data.IsFree) {
							var mainGameAppIdString = $"{ParseStrings.appIdPrefix}{fullGameAppDetails.Data.SteamAppID}";
							freeSubsIDString = string.IsNullOrEmpty(freeSubsIDString) ? mainGameAppIdString : string.Join(",", freeSubsIDString, mainGameAppIdString);
						} else _logger.LogDebug(ParseStrings.debugGetSteamSubIDMainGameNotFree, data.SteamAppID);
					} else _logger.LogDebug(ParseStrings.debugGetSteamSubIDMainGameAppDetailsFailed);
				} else _logger.LogDebug(ParseStrings.debugGetSteamSubIDNotDLC);

				_logger.LogDebug(ParseStrings.debugGetSteamSubIDNoSubID);
				return freeSubsIDString;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugGetSteamSubID}");
				return string.Empty;
			}
		}

		private static bool IsGOGGiveaway(string url) {
			return url.Contains(ParseStrings.gogGiveawayUrlKeyword) || url.TrimEnd('/').EndsWith(ParseStrings.gogGiveawayUrlEndKeyword);
		}

		private async Task<bool> IsClaimable(string url) {
			try {
				_logger.LogDebug($"{ParseStrings.debugCheckItchIOClaimable} | {url}");

				if (url.StartsWith(ParseStrings.itchSalePageUrlPrefix)) return true;

				var source = await services.GetRequiredService<Scraper>().GetSource(url);
				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(source);

				var buyButtons = htmlDoc.DocumentNode.SelectNodes(ParseStrings.itchioBuyButtonXPath);

				// var downloadButton = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.itchioDownloadButtonXPath);
				//result = buyButton != null && downloadButton == null && buyButton.InnerText.Contains(ParseStrings.itchioDownloadOrClaimText);

				_logger.LogDebug($"Done: {ParseStrings.debugCheckItchIOClaimable}");
				return buyButtons != null && buyButtons.Count > 0 && buyButtons.Any(button => button.InnerText.Contains(ParseStrings.itchioDownloadOrClaimText));
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugCheckItchIOClaimable} | {url}");
				return false;
			}
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
