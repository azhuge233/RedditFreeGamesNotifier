using System.Text.Json.Serialization;

namespace RedditFreeGamesNotifier.Models.SteamApi {
	public class QueryRewardItemsResponse {
		[JsonPropertyName("response")]
		public Response Response { get; set; }
	}

	public class Response {
		[JsonPropertyName("definitions")]
		public List<Definition> Definitions { get; set; }

		[JsonPropertyName("count")]
		public int Count { get; set; }

		//[JsonPropertyName("total_count")]
		//public int TotalCount { get; set; }

		//[JsonPropertyName("next_cursor")]
		//public string NextCursor { get; set; }
	}

	public class Definition {
		[JsonPropertyName("appid")]
		public int Appid { get; set; }

		[JsonPropertyName("defid")]
		public int Defid { get; set; }

		[JsonPropertyName("point_cost")]
		public string PointCost { get; set; }

		[JsonPropertyName("internal_description")]
		public string InternalDescription { get; set; }

		[JsonPropertyName("active")]
		public bool Active { get; set; }

		//[JsonPropertyName("type")]
		//public int Type { get; set; }

		//[JsonPropertyName("community_item_class")]
		//public int CommunityItemClass { get; set; }

		//[JsonPropertyName("community_item_type")]
		//public int CommunityItemType { get; set; }

		//[JsonPropertyName("timestamp_created")]
		//public long TimestampCreated { get; set; }

		//[JsonPropertyName("timestamp_updated")]
		//public long TimestampUpdated { get; set; }

		//[JsonPropertyName("timestamp_available")]
		//public long TimestampAvailable { get; set; }

		//[JsonPropertyName("timestamp_available_end")]
		//public long TimestampAvailableEnd { get; set; }

		//[JsonPropertyName("quantity")]
		//public string Quantity { get; set; }

		//[JsonPropertyName("community_item_data")]
		//public CommunityItemData CommunityItemData { get; set; }

		//[JsonPropertyName("usable_duration")]
		//public int UsableDuration { get; set; }

		//[JsonPropertyName("bundle_discount")]
		//public int BundleDiscount { get; set; }
	}

	//public class CommunityItemData {
	//	[JsonPropertyName("item_name")]
	//	public string ItemName { get; set; }

	//	[JsonPropertyName("item_title")]
	//	public string ItemTitle { get; set; }

	//	[JsonPropertyName("item_image_small")]
	//	public string ItemImageSmall { get; set; }

	//	[JsonPropertyName("item_image_large")]
	//	public string ItemImageLarge { get; set; }

	//	[JsonPropertyName("animated")]
	//	public bool Animated { get; set; }

	//	[JsonPropertyName("tiled")]
	//	public bool Tiled { get; set; }
	//}
}
