﻿namespace RedditFreeGamesNotifier.Strings {
	internal class ParseStrings {
		internal static readonly Dictionary<string, string> SupportedPlatform = new() {
			{ "store.steampowered.com", "Steam" },
			{ "steampowered.com", "Steam" },
			{ "gog.com", "GOG" },
			{ "itch.io", "Itch.io" },
			{ "store.ubi.com", "Ubisoft" }
		};

		internal static HashSet<string> ignoreKeywords = new() {
			"ended", "expired", "contains tasks", "discussion", "social media required"
		};

		internal static readonly string redditUrl = "https://old.reddit.com";
		internal static readonly string steamApiAppDetailsPrefix = "https://store.steampowered.com/api/appdetails?filters=basic,packages&appids=";

		internal static readonly string appIdRegex = @"app/[0-9]*";
		internal static readonly string subIdRegex = @"sub/[0-9]*";
		internal static readonly string subIdPrefix = "sub/";

		internal static readonly string steamAppDetailsGameTypeKey = "type";
		internal static readonly string steamAppDetailsGameNameKey = "name";
		internal static readonly string steamAppDetailsPackageGroupsKey = "package_groups";
		internal static readonly string steamAppDetailsPackagesKey = "packages";
		internal static readonly string steamAppDetailsGameTypeValueDefault = "default";

		internal static readonly string gogGiveawayUrlKeyword = "#giveaway";
		internal static readonly string gogGiveawayUrlEndKeyword = "gog.com";

		internal static readonly string gogAllGamesPageTitle = "DRM-free | GOG.COM";
		internal static readonly string gogRedirectedToMainPageTitle = "GOG.COM | GOG.COM";
		internal static readonly string gogFreePartnerUrl = "https://www.gog.com/partner/free_games";

		#region Xpaths
		internal static readonly string redditDivXPath = @".//div[contains(@class, 'thing') and contains(@class, 'link')]";
		internal static readonly string redditTitleLinkXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//a[contains(@class, 'title')]";
		internal static readonly string redditEndedPXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//span";
		internal static readonly string redditEndedSpanXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//span[contains(@class, 'linkflairlabel')]";

		internal static readonly string gogGameTitleXPath = @".//h1[contains(@class, 'productcard-basics__title')]";
		internal static readonly string gogTitleXPath = @".//title";

		internal static readonly string itchioBuyButtonXPath = @".//a[contains(@class, 'buy_btn')]";
		internal static readonly string itchioDownloadButtonXPath = @".//a[contains(@class, 'download_btn')]";
		// internal static readonly string itchioDownloadOrClaimText = @"Download or claim";
		internal static readonly string itchioDownloadOrClaimText = @"claim";
		#endregion

		#region data attributes key
		internal static readonly string dataDomainKey = "data-domain";
		internal static readonly string dataUrlKey = "data-url";
		internal static readonly string dataRedditPermaLinkKey = "data-permalink";
		#endregion

		#region debug strings
		internal static readonly string debugParse = "Parse";
		internal static readonly string debugParseWithUrl = "Parsing: {0}\n\n";

		internal static readonly string debugGetGameName = "GetGameName";
		internal static readonly string debugGetGameNameWithUrl = "GetGameName: {0}";
		internal static readonly string debugGetGameNameAppDetailsFailed = "Get name name failed since Steam app details failed, appID: {0}";

		internal static readonly string debugGetSteamSubID = "Get Steam Sub ID";
		internal static readonly string debugGotSteamSubID = "Got Steam Sub ID: {0}";
		internal static readonly string debugGetSteamSubIDNotDLC = "Not DLC, skip getting sub ID, returning app ID instead.";
		internal static readonly string debugGetSteamSubIDNoSubID = "No sub ID detected, returning empty string.";
		internal static readonly string debugGetSteamSubIDAppDetailsFailed = "Get sub ID failed since Steam app details failed.";

		internal static readonly string debugGetSteamAppDetails = "GetSteamAppDetails: {0}";

		internal static readonly string infoFoundNewGame = "Found new free game: {0}";
		internal static readonly string debugFoundInOldRecords = "Found {0} in old records, stop adding to push list";

		internal static readonly string debugIsGOGGiveaway = "GOG Giveaway detected: {0}";

		internal static readonly string debugSteamApiFailed = "Steam App Detail API for game ID {0} returned failed success code.";
		internal static readonly string errorGetSteamAppDetails = "Cannot get steam app details, app ID: {0}";
		internal static readonly string errorGetGameName = "Cannot fetch game name from: {0}, probably caused by poor internet connection.";

		internal static readonly string debugCheckItchIOClaimable = "Checking whether itch.io free game is claimable";

		internal static readonly string debugGOGGiveawayDuplication = "Skipping, same GOG giveaway was found in other source: {0}\n";
		internal static readonly string debugGOGDuplication = "Skipping, same GOG game was found in other source: {0}\n";
		internal static readonly string debugGOGFreePartnerPage = "Skipping, GOG free partner page detected: {0}\n";

		internal static readonly string debugSteamIDDuplicationInOldRecord = "Skipping, same App/Sub ID was found in old records: {0}\n";
		internal static readonly string debugSteamIDNotDetected = "Skipping, no Steam App/Sub ID detected: {0}\n";
		internal static readonly string debugSteamIDDuplicationDetected = "Skipping, same App/Sub ID was found in other source: {0}\n";

		internal static readonly string debugItchIOCNotClaimable = "Skipping, not claimable: {0}\n";
		internal static readonly string debugFoundInPreviousPage = "Skipping, found same record in other source: {0}\n";
		#endregion
	}
}
