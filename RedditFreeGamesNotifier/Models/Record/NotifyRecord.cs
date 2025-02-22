using System.Text;
using System.Text.RegularExpressions;
using RedditFreeGamesNotifier.Strings;

namespace RedditFreeGamesNotifier.Models.Record {
	public class NotifyRecord: FreeGameRecord {
		public NotifyRecord() { }

		public NotifyRecord(FreeGameRecord record): base(record) { }
		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, NotifyStrings.removeSpecialCharsRegex, string.Empty);
		}

		public string ToTelegramMessage() {
			if(Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.telegramPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl, RemoveSpecialCharacters(Name)).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.telegramPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl, RemoveSpecialCharacters(Name)).ToString();
		}

		public string ToBarkMessage() {
			if(Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.barkPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.barkPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToEmailMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.emailPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.emailPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToQQMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.qqPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.qqPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToPushPlusMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToDingTalkMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToPushDeerMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.pushDeerPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.pushDeerPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}

		public string ToDiscordMessage() {
			if (Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.discordPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.discordPushFormat[NotifyStrings.PlatformId[Platform]], Url, RedditUrl).ToString();
		}

		public string ToMeowMessage() { 
			if(Platform == "Steam") return new StringBuilder().AppendFormat(NotifyFormatStrings.meowPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl).ToString();
			return new StringBuilder().AppendFormat(NotifyFormatStrings.meowPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl).ToString();
		}
	}
}
