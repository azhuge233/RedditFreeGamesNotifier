namespace RedditFreeGamesNotifier.Strings {
	internal class ScrapeStrings {
		internal static List<string> RedditUrls { get; set; } = new() {
			"https://old.reddit.com/r/FreeGamesOnSteam/new/",
			"https://old.reddit.com/r/FreeGameFindings/new/",
			"https://old.reddit.com/r/freegames/new/",
			//"https://old.reddit.com/r/FreeGamesOnSteam/",
			//"https://old.reddit.com/r/FreeGameFindings/",
			//"https://old.reddit.com/r/freegames/"
		};

		internal static string[] UAs = new string[] {
			//"Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)",
			//"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; Googlebot/2.1; +http://www.google.com/bot.html) Chrome/112.0.0.0 Safari/537.36",
			"RedditApp/614658 CFNetwork/1494.0.7 Darwin/23.4.0",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64",
			"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:70.0) Gecko/20100101 Firefox/70.0",
			"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36 OPR/65.0.3467.48",
			"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36",
			"Reddit/Version 2024.18.0/Build 614658/iOS Version 17.4.1 (Build 21E236)"
		};

		#region debug strings
		internal static readonly string debugGetSource = "Get source";
		internal static readonly string debugGetSourceWithUrl = "Getting source: {0}";
		#endregion
	}
}
