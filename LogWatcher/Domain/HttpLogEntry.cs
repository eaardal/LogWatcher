using System;

namespace LogWatcher.Domain
{
    class HttpLogEntry : LogEntry
    {
        public string SourceApplication { get; set; }

        public HttpLogEntry()
        {
            
        }

        public HttpLogEntry(string sourceApplication, DateTime timestamp, string source, string text) : base(timestamp, source, text)
        {
            SourceApplication = sourceApplication;
        }

        public HttpLogEntry(string sourceApplication, DateTime timestamp, string severity, string source, string text)
            : base(timestamp, severity, source, text)
        {
            SourceApplication = sourceApplication;
        }
    }
}
