using RedditFreeGamesNotifier.Models.Record;

namespace RedditFreeGamesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = [];

		public List<FreeGameRecord> NotifyRecords { get; set; } = [];

		public bool HasGOGGiveaway { get; set; } = false;

		public List<FreeGameRecord> SteamFreeGames { get; set; } = [];
	}
}
