using LogWatcher.Domain.Helpers;

namespace LogWatcher.Domain
{
    class BasicLogEntry
    {
        public string SourceIdentifier { get; set; }
        public string Text { get; set; }
        
        public static BasicLogEntry Parse(IBasicLogEntryFormat entryFormat, string identifier, string text)
        {
            return entryFormat.Parse(identifier, text);
        }

        public override string ToString()
        {
            return Text;
        }

        public static string GetAsJsonFormat()
        {
            var logEntry = new BasicLogEntry
            {
                SourceIdentifier = "The name of the application which generated the log entry",
                Text = "The log message content"
            };

            return JsonHelpers.ConvertToPrettifiedJson(logEntry);
        }
    }
}