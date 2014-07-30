namespace LogWatcher.Domain.Messages
{
    class ReceivedHttpLogEntryMessage<TLogEntry> where TLogEntry : BasicLogEntry
    {
        public TLogEntry LogEntry { get; set; }
    }
}
