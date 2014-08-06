namespace LogWatcher.Domain.Messages
{
    public class StatusBarMessage : LogWatcherMessage
    {
        public StatusBarMessage(string identifier) : base(identifier)
        {
        }

        public string Text { get; set; }
    }
}