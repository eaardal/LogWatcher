using System.Collections.Generic;

namespace LogWatcher.Domain.Messages
{
    class NewLogEntriesMessage<TLogEntry> : LogWatcherMessage
    {
        public NewLogEntriesMessage(string identifier) : base(identifier)
        {
        }

        public IEnumerable<TLogEntry> LogEntries { get; set; }
    }
}
