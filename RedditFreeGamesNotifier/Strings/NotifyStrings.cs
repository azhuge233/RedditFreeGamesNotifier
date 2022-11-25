namespace RedditFreeGamesNotifier.Strings {
	internal class NotifyStrings {
		internal static Dictionary<string, int> PlatformId { get; set; } = new Dictionary<string, int>() {
			{ "Steam", 0 }, { "GOG", 1 }, { "Itch.io", 2 }, { "Ubisoft", 3 }
		};
		internal static readonly string removeSpecialCharsRegex = @"[^0-9a-zA-Z]+";
	}
}
