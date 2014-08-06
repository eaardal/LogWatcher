namespace LogWatcher.Domain.Messages
{
    class ShowLoadingScreenMessage : LogWatcherMessage
    {
        public ShowLoadingScreenMessage(string identifier) : base(identifier)
        {
        }

        public string Message { get; set; }
    }
}
