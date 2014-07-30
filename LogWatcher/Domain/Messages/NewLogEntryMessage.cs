namespace LogWatcher.Domain.Messages
{
    class NewLogEntryMessage<TLogEntry> where TLogEntry : BasicLogEntry
    {
        public TLogEntry LogEntry { get; set; }
    }
}
