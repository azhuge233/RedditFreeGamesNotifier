namespace RedditFreeGamesNotifier.Strings {
	internal class NotifyFormatStrings {
		#region record strings
		internal static readonly List<string> telegramPushFormat = new() {
			"<b>{0}</b>\n\n" +
			"App/Sub/Def ID: <code><i>{1}</i></code>\n" +
			"Steam Link: <a href=\"{2}\" >{0}</a>\n" +
			"Reddit Link: {3}\n\n" +
			"#Reddit #Steam #{4}",

			"<b>{0}</b>\n\n" +
			"GOG Link: <a href=\"{1}\" >{0}</a>\n" +
			"Reddit Link: {2}\n\n" +
			"#Reddit #GOG #{3}",

			"<b>{0}</b>\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n\n" +
			"#Reddit #Itchio #{3}",

			"<b>{0}</b>\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n\n" +
			"#Reddit #Ubisoft #{3}",
		};

		internal static readonly List<string> barkPushFormat = new() {
			"{0}\n\n" +
			"App/Sub/Def ID: {1}\n" +
			"Steam Link: {2}\n" +
			"Reddit Link: {3}\n",

			"{0}\n\n" +
			"GOG Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n",
		};

		internal static readonly List<string> emailPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"App/Sub/Def ID: {1}<br>" +
			"Steam Link: <a href=\"{2}\" > {0}</a><br>" +
			"Reddit Link: {3}<br>",

			"<p><b>{0}</b><br>" +
			"GOG Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Itch.io Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Ubisoft Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>"
		};

		internal static readonly List<string> qqPushFormat = new() {
			"{0}\n\n" +
			"App/Sub/Def ID: {1}\n" +
			"Steam Link: {2}\n" +
			"Reddit Link: {3}\n",

			"{0}\n\n" +
			"GOG Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n"
		};

		internal static readonly List<string> pushPlusPushHtmlFormat = new() {
			"<p><b>{0}</b><br>" +
			"App/Sub/Def ID: {1}<br>" +
			"Steam Link: <a href=\"{2}\" > {0}</a><br>" +
			"Reddit Link: {3}<br>",

			"<p><b>{0}</b><br>" +
			"GOG Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Itch.io Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>",

			"<p><b>{0}</b><br>" +
			"Ubisoft Link: <a href=\"{1}\" > {0}</a><br>" +
			"Reddit Link: {2}<br>"
		};

		internal static readonly List<string> dingTalkPushFormat = new() {
			"{0}\n\n" +
			"App/Sub/Def ID: {1}\n" +
			"Steam Link: {2}\n" +
			"Reddit Link: {3}\n",

			"{0}\n\n" +
			"GOG Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n"
		};

		internal static readonly List<string> pushDeerPushFormat = new() {
			"{0}\n\n" +
			"App/Sub/Def ID: {1}\n" +
			"Steam Link: {2}\n" +
			"Reddit Link: {3}\n",

			"{0}\n\n" +
			"GOG Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n"
		};

		internal static readonly List<string> discordPushFormat = new() {
			"App/Sub/Def ID: {0}\n" +
			"Steam Link: {1}\n" +
			"Reddit Link: {2}\n\n",

			"GOG Link: {0}\n" +
			"Reddit Link: {1}\n\n",

			"Itch.io Link: {0}\n" +
			"Reddit Link: {1}\n\n",

			"Ubisoft Link: {0}\n" +
			"Reddit Link: {1}\n\n"
		};

		internal static readonly List<string> meowPushFormat = new() {
			"{0}\n\n" +
			"App/Sub/Def ID: {1}\n" +
			"Steam Link: {2}\n" +
			"Reddit Link: {3}\n",

			"{0}\n\n" +
			"GOG Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Itch.io Link: {1}\n" +
			"Reddit Link: {2}\n",

			"{0}\n\n" +
			"Ubisoft Link: {1}\n" +
			"Reddit Link: {2}\n",
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

		internal static readonly string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
		internal static readonly string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
		internal static readonly string qqWebSocketSendAction = "send_private_msg";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - RedditFreeGamesNotifier";
		internal static readonly string pushPlusASFTitleFormat = "RedditFreeGamesNotifierASFResult";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusGetUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";

		internal static readonly string meowUrlFormat = "{0}/{1}";
		internal static readonly string meowUrlTitle = "RedditFreeGamesNotifier";
		internal static readonly string meowUrlASFTitle = "RedditFreeGamesNotifierASFResult";
		#endregion

		internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/RedditFreeGamesNotifier";
		internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/RedditFreeGamesNotifier\">RedditFreeGamesNotifier</a>";
	}
}
