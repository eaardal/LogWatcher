using System.IO;

namespace LogWatcher.Domain.Messages
{
    class FileChangeDetectedMessage
    {
        public FilePoller Sender { get; set; }
        public FileInfo File { get; set; }
    }
}