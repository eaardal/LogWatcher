using System;

namespace LogWatcher.Domain.Helpers
{
    class LogEntryValidator
    {
        public bool IsValid(BasicLogEntry logEntry)
        {
            return HasValue(logEntry.SourceIdentifier) && HasValue(logEntry.Text);
        }

        public bool IsValid(LogEntry logEntry)
        {
            return IsValid((BasicLogEntry)logEntry) && HasValue(logEntry.Source) && HasValue(logEntry.Severity) && logEntry.Timestamp > DateTime.MinValue && logEntry.Timestamp < DateTime.MaxValue;
        }

        private bool HasValue(string text)
        {
            return !String.IsNullOrEmpty(text);
        }
    }
}
