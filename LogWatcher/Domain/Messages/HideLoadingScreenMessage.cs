namespace LogWatcher.Domain.Messages
{
    class HideLoadingScreenMessage : LogWatcherMessage
    {
        public HideLoadingScreenMessage(string identifier) : base(identifier)
        {
        }
    }
}
