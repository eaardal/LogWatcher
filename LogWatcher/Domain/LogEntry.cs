using System;

namespace LogWatcher.Domain
{
    class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

        public LogEntry()
        {
            
        }

        public LogEntry(DateTime timestamp, string source, string text) : this(timestamp, "None", source, text)
        {
        }

        public LogEntry(DateTime timestamp, string severity, string source, string text)
        {
            if (severity == null) throw new ArgumentNullException("severity");
            if (source == null) throw new ArgumentNullException("source");
            if (text == null) throw new ArgumentNullException("text");
            Timestamp = timestamp;
            Severity = severity;
            Source = source;
            Text = text;
        }

        public static LogEntry Parse(ILogEntryFormat entryFormat, string text)
        {
            return entryFormat.Parse(text);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}\t\t{3}", Severity, Timestamp, Source, Text);
        }
    }
}
