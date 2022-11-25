namespace RedditFreeGamesNotifier.Models.Config {
    public class ASFConfig : NotifyConfig {
        public bool EnableASF { get; set; }
        public bool NotifyASFResult { get; set; }
        public string ASFIPCUrl { get; set; }
        public string ASFIPCPassword { get; set; }
    }
}
