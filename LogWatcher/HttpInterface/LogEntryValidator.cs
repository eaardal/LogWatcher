using System;
using LogWatcher.Domain;

namespace LogWatcher.HttpInterface
{
    class LogEntryValidator
    {
        public bool IsValid(LogEntry logEntry)
        {
            return HasValue(logEntry.SourceIdentifier) && HasValue(logEntry.Source) && HasValue(logEntry.Text) && HasValue(logEntry.Severity) &&
                   logEntry.Timestamp > DateTime.MinValue && logEntry.Timestamp < DateTime.MaxValue;
        }

        private bool HasValue(string text)
        {
            return !String.IsNullOrEmpty(text);
        }
    }
}
