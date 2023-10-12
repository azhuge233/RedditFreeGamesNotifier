using System.Text.Json.Serialization;

namespace RedditFreeGamesNotifier.Models.WebSocketContent {
	public class WSPacket {
		[JsonPropertyName("type")]
		public string Type { get; set; }
		[JsonPropertyName("payload")]
		public object Payload { get; set; }
	}

	#region Connect Classes
	public class ConnectPayload {
		[JsonPropertyName("token")]
		public string Token { get; set; }
	}
	#endregion

	#region Message Classes
	public class MessagePayload {
		[JsonPropertyName("peer")]
		public Peer Peer { get; set; }
		[JsonPropertyName("elements")]
		public List<object> Elements { get; set; }
	}

	public class Peer {
		[JsonPropertyName("chatType")]
		public int ChatType { get; set; }
		[JsonPropertyName("peerUin")]
		public string PeerUin { get; set; }
	}

	public class TextElementRoot {
		[JsonPropertyName("elementType")]
		public int ElementType { get; set; } = 1;
		[JsonPropertyName("textElement")]
		public TextElement TextElement { get; set; }
	}

	public class TextElement {
		[JsonPropertyName("content")]
		public string Content { get; set; }
	}

	//public class ReplyElementRoot {
	//	[JsonPropertyName("elementType")]
	//	public int ElementType { get; set; } = 7;
	//	[JsonPropertyName("replyElement")]
	//	public ReplyElement ReplyElement { get; set; }
	//}

	//public class ReplyElement {
	//	[JsonPropertyName("replayMsgSeq")]
	//	public string ReplayMsgSeq { get; set; }
	//	[JsonPropertyName("sourceMsgIdInRecords")]
	//	public string SourceMsgIdInRecords { get; set; }
	//	[JsonPropertyName("senderUid")]
	//	public string SenderUid { get; set; }
	//}
	#endregion
}
