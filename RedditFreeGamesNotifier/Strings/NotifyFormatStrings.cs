namespace RedditFreeGamesNotifier.Strings {
	internal class NotifyFormatStrings {
		#region record strings
		internal static readonly List<string> telegramPushFormat = new() {
			"<b>{0}</b>\n\n" +
			"App/Sub ID: <code><i>{1}</i></code>\n" +
			"Steam 链接: <a href=\"{2}\" >{0}</a>\n" +
			"Reddit 链接: {3}\n\n" +
			"#Reddit #Steam #{4}",

			"<b>{0}</b>\n\n" +
			"GOG 链接: <a href=\"{1}\" >{0}</a>\n" +
			"Reddit 链接: {2}\n\n" +
			"#Reddit #GOG #{3}",

			"<b>{0}</b>\n\n" +
			"Itch.io 链接: {1}\n" +
			"Reddit 链接: {2}\n\n" +
			"#Reddit #Itchio #{3}",

			"<b>{0}</b>\n\n" +
			"Ubisoft 链接: {1}\n" +
			"Reddit 链接: {2}\n\n" +
			"#Reddit #Ubisoft #{3}",
		};

		internal static readonly List<string> barkPushFormat = new() {
			"{0}\n\n" +
			"App/Sub ID: {1}\n" +
			"Steam 链接: {2}\n" +
			"Reddit 链接: {3}\n",

			"{0}\n\n" +
			"GOG 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Itch.io 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Ubisoft 链接: {1}\n" +
			"Reddit 链接: {2}\n",
		};

		internal static readonly List<string> emailPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"App/Sub ID: {1}<br>" +
			"Steam 链接: <a href=\"{2}\" > {0}</a><br>" +
			"Reddit 链接: {3}<br>",

			"<p><b>{0}</b><br>" +
			"GOG 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Itch.io 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Ubisoft 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>"
		};

		internal static readonly List<string> qqPushFormat = new() {
			"{0}\n\n" +
			"App/Sub ID: {1}\n" +
			"Steam 链接: {2}\n" +
			"Reddit 链接: {3}\n",

			"{0}\n\n" +
			"GOG 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Itch.io 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Ubisoft 链接: {1}\n" +
			"Reddit 链接: {2}\n"
		};

		internal static readonly List<string> pushPlusPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"App/Sub ID: {1}<br>" +
			"Steam 链接: <a href=\"{2}\" > {0}</a><br>" +
			"Reddit 链接: {3}<br>",

			"<p><b>{0}</b><br>" +
			"GOG 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Itch.io 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Ubisoft 链接: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit 链接: {2}<br>"
		};

		internal static readonly List<string> dingTalkPushFormat = new() {
			"{0}\n\n" +
			"App/Sub ID: {1}\n" +
			"Steam 链接: {2}\n" +
			"Reddit 链接: {3}\n",

			"{0}\n\n" +
			"GOG 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Itch.io 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Ubisoft 链接: {1}\n" +
			"Reddit 链接: {2}\n"
		};

		internal static readonly List<string> pushDeerPushFormat = new() {
			"{0}\n\n" +
			"App/Sub ID: {1}\n" +
			"Steam 链接: {2}\n" +
			"Reddit 链接: {3}\n",

			"{0}\n\n" +
			"GOG 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Itch.io 链接: {1}\n" +
			"Reddit 链接: {2}\n",

			"{0}\n\n" +
			"Ubisoft 链接: {1}\n" +
			"Reddit 链接: {2}\n"
		};

		internal static readonly List<string> discordPushFormat = new() {
			"App/Sub ID: {0}\n" +
			"Steam 链接: {1}\n" +
			"Reddit 链接: {2}\n\n",

			"GOG 链接: {0}\n" +
			"Reddit 链接: {1}\n\n",

			"Itch.io 链接: {0}\n" +
			"Reddit 链接: {1}\n\n",

			"Ubisoft 链接: {0}\n" +
			"Reddit 链接: {1}\n\n"
		};
		#endregion

		#region url, title format string
		internal static readonly string barkUrlFormat = "{0}/{1}/";
		internal static readonly string barkUrlTitle = "RedditFreeGamesNotifier/";
		internal static readonly string barkUrlASFTitle = "RedditFreeGamesNotifierASFResult/";
		internal static readonly string barkUrlArgs =
			"?group=redditfreegames" +
			"&isArchive=1" +
			"&sound=calypso";

		internal static readonly string emailTitleFormat = "{0} new free game(s) - RedditFreeGamesNotifier";
		internal static readonly string emailASFTitleFormat = "RedditFreeGamesNotifierASFResult";
		internal static readonly string emailBodyFormat = "<br>{0}";

		internal static readonly string qqUrlFormat = "http://{0}:{1}/send_private_msg?user_id={2}&message=";
		internal static readonly string qqRedUrlFormat = "ws://{0}:{1}";
		internal static readonly string qqRedWSConnectPacketType = "meta::connect";
		internal static readonly string qqRedWSSendPacketType = "message::send";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - RedditFreeGamesNotifier";
		internal static readonly string pushPlusASFTitleFormat = "RedditFreeGamesNotifierASFResult";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusGetUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";
		#endregion

		internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/RedditFreeGamesNotifier";
		internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/RedditFreeGamesNotifier\">RedditFreeGamesNotifier</a>";
	}
}
