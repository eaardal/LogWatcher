using System;
using LogWatcher.Domain.Helpers;

namespace LogWatcher.Domain
{
    class LogEntry : BasicLogEntry
    {
        public string Severity { get; set; }
        public string Source { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}\t\t{3}", Severity, Timestamp, Source, Text);
        }

        public new static string GetAsJsonFormat()
        {
            var logEntry = new LogEntry
            {
                SourceIdentifier = "The name of the application which generated the log entry",
                Severity = "The severity of the message",
                Source = "The source of the log entry (class/file name)",
                Text = "The actual log message text",
                Timestamp = DateTime.Now
            };

            return JsonHelpers.ConvertToPrettifiedJson(logEntry);
        }
    }
}
