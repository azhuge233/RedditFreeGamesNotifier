namespace RedditFreeGamesNotifier.Strings {
	internal class ParseStrings {
		#region platform 
		internal static readonly Dictionary<string, string> SupportedPlatform = new() {
			{ "store.steampowered.com", "Steam" },
			{ "steampowered.com", "Steam" },
			{ "gog.com", "GOG" },
			{ "itch.io", "Itch.io" },
			{ "store.ubi.com", "Ubisoft" }
		};
		#endregion

		#region keyword blacklist
		internal static HashSet<string> ignoreKeywords = [
			"ended", "expired", "contains tasks", "discussion", "social media required"
		];
		#endregion

		internal static readonly string redditUrl = "https://old.reddit.com";

		#region Steam
		internal static string gameTypeGame = "game";
		internal static string gameTypeDLC = "dlc";
		internal static HashSet<string> supportedGameType = [ gameTypeGame, gameTypeDLC];

		internal static readonly string steamApiAppDetailsPrefix = "https://store.steampowered.com/api/appdetails?filters=basic,packages&appids=";

		internal static readonly string steamPointsShopUrlPrefix = "https://store.steampowered.com/points/shop";
		internal static readonly string steamPointsShopItem = "Steam Points Shop Item";

		internal static readonly string steamQueryRewardItemsApiPrefix = "https://api.steampowered.com/ILoyaltyRewardsService/QueryRewardItems/v1/?appids[0]=";

		internal static readonly string appIdRegex = @"app/[0-9]*";
		internal static readonly string subIdRegex = @"sub/[0-9]*";
		internal static readonly string appIdPrefix = "app/";
		internal static readonly string subIdPrefix = "sub/";

		internal static readonly string steamAppDetailsGameTypeKey = "type";
		internal static readonly string steamAppDetailsGameNameKey = "name";
		internal static readonly string steamAppDetailsPackageGroupsKey = "package_groups";
		internal static readonly string steamAppDetailsPackagesKey = "packages";
		internal static readonly string steamAppDetailsGameTypeValueDefault = "default";
		#endregion

		#region GOG
		internal static readonly string gogGiveawayUrlKeyword = "#giveaway";
		internal static readonly string gogGiveawayUrlEndKeyword = "gog.com";

		internal static readonly string gogAllGamesPageTitle = "DRM-free | GOG.COM";
		internal static readonly string gogRedirectedToMainPageTitle = "GOG.COM | GOG.COM";
		internal static HashSet<string> gogIgnoredUrls = [
			"https://www.gog.com/partner/free_games",
			"https://www.gog.com/account",
			"https://www.gog.com/redeem"
		];
		#endregion

		#region itch.io
		internal static readonly string itchSalePageUrlPrefix = "https://itch.io/s/";
		#endregion

		#region Xpaths
		internal static readonly string redditDivXPath = @".//div[contains(@class, 'thing') and contains(@class, 'link') and not(contains(@class, 'promoted'))]";
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
		internal static readonly string debugNoFreeSubID = "No free sub detected.";
		internal static readonly string debugGetSteamSubIDNotDLCOrGame = "Not DLC or game, skip getting sub ID, returning app ID instead.";
		internal static readonly string debugGetSteamSubIDNoSubID = "No sub ID detected, returning empty string.";
		internal static readonly string debugGetSteamSubIDMainGameAppID = "Getting main game AppID";
		internal static readonly string debugGetSteamSubIDMainGameNotFree = "Main game for DLC app/{0} is not free, skipping";
		internal static readonly string debugGetSteamSubIDMainGameAppDetailsFailed = "Get main game app ID failed since getting Steam app details failed.";
		internal static readonly string debugGetSteamSubIDAppDetailsFailed = "Get sub ID failed since null Steam app details or failed success code.";

		internal static readonly string debugGetSteamAppDetails = "GetSteamAppDetails: {0}";

		internal static readonly string infoFoundNewGame = "Found new free game: {0}";
		internal static readonly string debugFoundInOldRecords = "Found {0} in old records, stop adding to push list";

		internal static readonly string debugIsGOGGiveaway = "GOG Giveaway detected: {0}";

		internal static readonly string errorGetSteamAppDetails = "Cannot get steam app details, app ID: {0}";
		internal static readonly string errorGetGameName = "Cannot fetch game name from: {0}, probably caused by poor internet connection.";

		internal static readonly string debugCheckItchIOClaimable = "Checking whether itch.io free game is claimable";

		internal static readonly string debugGOGGiveawayDuplication = "Skipping, same GOG giveaway was found in other source: {0}\n";
		internal static readonly string debugGOGDuplication = "Skipping, same GOG game was found in other source: {0}\n";
		internal static readonly string debugGOGFreePartnerPage = "Skipping, GOG free partner page/account/redeem detected: {0}\n";

		internal static readonly string debugSteamIDDuplicationInOldRecord = "Skipping, same App/Sub ID was found in old records: {0}\n";
		internal static readonly string debugSteamIDNotDetected = "Skipping, no Steam App/Sub ID detected: {0}\n";
		internal static readonly string debugSteamIDDuplicationDetected = "Skipping, same App/Sub ID was found in other source: {0}\n";

		internal static readonly string debugItchIOCNotClaimable = "Skipping, not claimable: {0}\n";
		internal static readonly string debugFoundInPreviousPage = "Skipping, found same record in other source: {0}\n";

		internal static readonly string debugSteamPointsShtopItemDetected = "Steam points shop item detected: {0}\n";

		internal static readonly string debugNoSteamFestivalAppIDDetected = "No Steam Festival AppID detected!";
		internal static readonly string debugGetPointShopItemDefId = "Get point shop item DefID";
		internal static readonly string errorGetPointShopItemDefIdFailed = "Cannot get point shop item DefID: {0}";

		internal static readonly string infoTotalDivCount = "Total div count: {0}";
		internal static readonly string errorNoDivDetected = "No div detected.";
		#endregion
	}
}
