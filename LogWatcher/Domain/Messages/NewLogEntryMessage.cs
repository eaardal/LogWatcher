namespace LogWatcher.Domain.Messages
{
    class NewLogEntryMessage<TLogEntry> : LogWatcherMessage where TLogEntry : BasicLogEntry
    {
        public NewLogEntryMessage(string identifier) : base(identifier)
        {
        }

        public TLogEntry LogEntry { get; set; }
    }
}
