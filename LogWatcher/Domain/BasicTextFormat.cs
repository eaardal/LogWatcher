using System;

namespace LogWatcher.Domain
{
    internal class BasicTextFormat : ILogEntryFormat
    {
        public LogEntry Parse(string identifier, string text)
        {
            return new LogEntry {Timestamp = DateTime.Now, Text = text, SourceIdentifier = identifier};
        }
    }
}