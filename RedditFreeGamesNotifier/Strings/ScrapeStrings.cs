namespace RedditFreeGamesNotifier.Strings {
	internal class ScrapeStrings {
		internal static List<string> RedditUrls { get; set; } = new() {
			"https://old.reddit.com/r/FreeGamesOnSteam/new/",
			"https://old.reddit.com/r/FreeGameFindings/new/",
			"https://old.reddit.com/r/freegames/new/",
			"https://old.reddit.com/r/FreeGamesOnSteam/",
			"https://old.reddit.com/r/FreeGameFindings/",
			"https://old.reddit.com/r/freegames/"
		};

		#region debug strings
		internal static readonly string debugGetSource = "Get source";
		internal static readonly string debugGetSourceWithUrl = "Getting source: {0}";
		#endregion
	}
}
