using System.Text.Json.Serialization;

namespace RedditFreeGamesNotifier.Models.SteamApi {
	public class AppDetails {
		[JsonPropertyName("success")]
		public bool Success { get; set; }
		[JsonPropertyName("data")]
		public Dictionary<string, object> Data { get; set; }
	}
}
