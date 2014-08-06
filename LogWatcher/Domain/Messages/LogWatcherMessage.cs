using System;

namespace LogWatcher.Domain.Messages
{
    public abstract class LogWatcherMessage
    {
        public string Identifier { get; private set; }
        public long TimestampTicks { get; private set; }

        protected LogWatcherMessage(string identifier)
        {
            Identifier = identifier;
            TimestampTicks = DateTime.Now.Ticks;
        }
    }
}
