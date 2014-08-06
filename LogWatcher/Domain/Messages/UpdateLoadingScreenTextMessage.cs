namespace LogWatcher.Domain.Messages
{
    class UpdateLoadingScreenTextMessage : LogWatcherMessage
    {
        public string Msg { get; set; }

        public UpdateLoadingScreenTextMessage(string identifier, string msg) : base(identifier)
        {
            Msg = msg;
        }
    }
}
