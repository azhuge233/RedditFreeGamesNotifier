using RedditFreeGamesNotifier.Models.Config;
using RedditFreeGamesNotifier.Models.Record;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal interface INotifiable : IDisposable {
		public Task SendMessage(NotifyConfig config, List<NotifyRecord> records);
	}
}
