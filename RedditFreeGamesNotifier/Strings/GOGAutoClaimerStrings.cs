namespace RedditFreeGamesNotifier.Strings {
	internal class GOGAutoClaimerStrings {
		internal static readonly string Url = "https://www.gog.com/giveaway/claim";

		internal static readonly string UAKey = "user-agent";
		internal static readonly string UAValue = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.27";
		internal static readonly string CookieKey = "cookie";

		#region debug strings
		internal static readonly string debugClaim = "GOG Claim";
		internal static readonly string infoClaiming = "Claiming: {0}";
		internal static readonly string infoDisabled = "GOG auto claim not enabled, skipping";
		internal static readonly string infoNoRecords = "No GOG giveaway record, skipping auto claim";
		#endregion
	}
}
