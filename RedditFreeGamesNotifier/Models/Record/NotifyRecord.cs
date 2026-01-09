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
			if(Platform == "Steam") return string.Format(NotifyFormatStrings.telegramPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl, RemoveSpecialCharacters(Name));
			return string.Format(NotifyFormatStrings.telegramPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl, RemoveSpecialCharacters(Name));
		}

		public string ToBarkMessage() {
			if(Platform == "Steam") return string.Format(NotifyFormatStrings.barkPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.barkPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToEmailMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.emailPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.emailPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToQQMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.qqPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.qqPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToPushPlusMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.pushPlusPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.pushPlusPushHtmlFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToDingTalkMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.dingTalkPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.dingTalkPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToPushDeerMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.pushDeerPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.pushDeerPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}

		public string ToDiscordMessage() {
			if (Platform == "Steam") return string.Format(NotifyFormatStrings.discordPushFormat[NotifyStrings.PlatformId[Platform]], AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.discordPushFormat[NotifyStrings.PlatformId[Platform]], Url, RedditUrl);
		}

		public string ToMeowMessage() { 
			if(Platform == "Steam") return string.Format(NotifyFormatStrings.meowPushFormat[NotifyStrings.PlatformId[Platform]], Name, AppId, Url, RedditUrl);
			return string.Format(NotifyFormatStrings.meowPushFormat[NotifyStrings.PlatformId[Platform]], Name, Url, RedditUrl);
		}
	}
}
