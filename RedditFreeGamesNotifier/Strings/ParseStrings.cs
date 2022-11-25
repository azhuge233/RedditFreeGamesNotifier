using System.Runtime.CompilerServices;

namespace RedditFreeGamesNotifier.Strings {
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
		internal static readonly string steamApiurlPrefix = "https://store.steampowered.com/api/appdetails?appids=";

		internal static readonly string appIdRegex = @"app/[0-9]*";
		internal static readonly string subIdRegex = @"sub/[0-9]*";

		internal static readonly string steamAppDetailGameNameKey = "name";
		internal static readonly string gogGiveawayUrlKeyword = "#giveaway";

		#region Xpaths
		internal static readonly string redditDivXPath = @".//div[contains(@class, 'thing') and contains(@class, 'link')]";
		internal static readonly string redditTitleLinkXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//a[contains(@class, 'title')]";
		internal static readonly string redditEndedPXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//span";
		internal static readonly string redditEndedSpanXPath = @".//div[contains(@class, 'entry')]//p[contains(@class, 'title')]//span[contains(@class, 'linkflairlabel')]";

		internal static readonly string gogGameTitleXPath = @".//h1[contains(@class, 'productcard-basics__title')]";
		#endregion

		#region data attributes key
		internal static readonly string dataDomainKey = "data-domain";
		internal static readonly string dataUrlKey = "data-url";
		internal static readonly string dataRedditPermaLinkKey = "data-permalink";
		#endregion

		#region debug strings
		internal static readonly string debugParse = "Parse";
		internal static readonly string debugParseWithUrl = "Parsing: {0}";
		internal static readonly string debugGetGameName = "GetGameName";
		internal static readonly string debugGetGameNameWithUrl = "GetGameName: {0}";

		internal static readonly string infoFoundNewGame = "Found new free game: {0}";
		internal static readonly string debugFoundInOldRecords = "Found {0} in old records, stop adding to push list";

		internal static readonly string debugIsGOGGiveaway = "GOG Giveaway detected: {0}";
		#endregion
	}
}
