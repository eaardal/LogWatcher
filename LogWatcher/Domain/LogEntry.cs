using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LogWatcher.Domain
{
    class LogEntry
    {
        public string SourceIdentifier { get; set; }
        public DateTime Timestamp { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }
        
        public static LogEntry Parse(ILogEntryFormat entryFormat, string identifier, string text)
        {
            return entryFormat.Parse(identifier, text);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}\t\t{3}", Severity, Timestamp, Source, Text);
        }

        public static string GetAsJsonFormat()
        {
            var logEntry = new LogEntry
            {
                SourceIdentifier = "The name of the application which generated the log entry",
                Severity = "The severity of the message",
                Source = "The source of the log entry (class/file name)",
                Text = "The actual log message text",
                Timestamp = DateTime.Now
            };
                               
            return JsonConvert.SerializeObject(logEntry, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
