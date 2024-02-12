using System.Text.Json.Serialization;

namespace RedditFreeGamesNotifier.Models.SteamApi {
	public class AppDetails {
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("data")]
		public Data Data { get; set; }
	}

	public class Data {
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("is_free")]
		public bool IsFree { get; set; }

		[JsonPropertyName("packages")]
		public List<int> Packages { get; set; } = [];

		[JsonPropertyName("package_groups")]
		public List<PackageGroup> PackageGroups { get; set; } = [];
	}

	public class PackageGroup {
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("subs")]
		public List<Sub> Subs { get; set; } = [];
	}

	public class Sub {
		[JsonPropertyName("packageid")]
		public int PackageID { get; set; }

		[JsonPropertyName("percent_savings")]
		public int PercentSavings { get; set; }

		[JsonPropertyName("is_free_license")]
		public bool IsFreeLicense { get; set; }
	}
}
