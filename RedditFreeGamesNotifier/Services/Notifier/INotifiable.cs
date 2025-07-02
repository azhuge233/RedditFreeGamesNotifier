using RedditFreeGamesNotifier.Models.Record;

namespace RedditFreeGamesNotifier.Services.Notifier {
	internal interface INotifiable : IDisposable {
		public Task SendMessage(List<NotifyRecord> records);
		public Task SendMessage(string asfRecord);
	}
}
