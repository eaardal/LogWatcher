namespace LogWatcher.Domain.Messages
{
    class ShowLoadingScreenMessage
    {
        public string Identifier { get; set; }
        public string Message { get; set; }
        public int DurationMilliseconds { get; set; }
        public bool UseDuration { get; set; }
    }
}
