using System;
using LogWatcher.Domain.Helpers;
using Newtonsoft.Json;

namespace LogWatcher.Domain
{
    class BasicLogEntry
    {
        public string SourceIdentifier { get; set; }
        public string Text { get; set; }

        [JsonIgnore]
        public int LineNr { get; set; }

        public static BasicLogEntry Parse(IBasicLogEntryFormat entryFormat, string identifier, string text, int lineNr)
        {
            return entryFormat.Parse(identifier, text, lineNr);
        }

        public override string ToString()
        {
            //return PrintWithLineNr();
            return PrintText();
        }

        public string PrintText()
        {
            return Text;
        }

        public string PrintWithLineNr()
        {
            return String.Format("#{0}: {1}", LineNr, Text);
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