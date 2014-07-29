using System;
using System.IO;

namespace LogWatcher.Domain
{
    internal interface ILogService
    {
        Action<LogEntry> NewLogEntryCallback { get; set; }
    }
}