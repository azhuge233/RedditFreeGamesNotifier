using RedditFreeGamesNotifier.Models.Record;

namespace RedditFreeGamesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = new List<FreeGameRecord>();

		public List<FreeGameRecord> NotifyRecords { get; set; } = new List<FreeGameRecord>();

		public bool HasGOGGiveaway { get; set; } = false;

		public List<FreeGameRecord> SteamFreeGames { get; set; } = new List<FreeGameRecord>();
	}
}
