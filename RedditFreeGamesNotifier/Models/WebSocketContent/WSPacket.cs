using System.Text.Json.Serialization;

namespace RedditFreeGamesNotifier.Models.WebSocketContent {
	public class WSPacket {
		[JsonPropertyName("action")]
		public string Action { get; set; }

		[JsonPropertyName("params")]
		public Param Params { get; set; }
	}

	public class Param {
		[JsonPropertyName("user_id")]
		public string UserID { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
