namespace LogWatcher.Domain.Messages
{
    class PollIntervalNotValidMessage
    {
        public object Sender { get; set; }
        public string PollInterval { get; set; }
    }
}
