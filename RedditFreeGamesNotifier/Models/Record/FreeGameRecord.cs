namespace RedditFreeGamesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Name { get; set; }
		public string Url { get; set; }
		public string RedditUrl { get; set; }
		public string Platform { get; set; }
		public string AppId { get; set; }

		public FreeGameRecord() { }

		public FreeGameRecord(FreeGameRecord record) {
			Name = record.Name;
			Url = record.Url;
			RedditUrl = record.RedditUrl;
			Platform = record.Platform;
			AppId = record.AppId;
		}
	}
}
