namespace LogWatcher.Domain.Messages.ErrorMessages
{
    class PollIntervalNotValidMessage
    {
        public object Sender { get; set; }
        public string PollInterval { get; set; }
    }
}
