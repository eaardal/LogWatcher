using System;
using System.IO;

namespace LogWatcher.Domain.Messages
{
    class FilePollTickMessage
    {
        public FilePoller Sender { get; set; }
        public FileInfo File { get; set; }
        public DateTime Timestamp { get; set; }
    }
}