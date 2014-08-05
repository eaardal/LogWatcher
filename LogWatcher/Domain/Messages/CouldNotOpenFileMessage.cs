using System.IO;

namespace LogWatcher.Domain.Messages
{
    internal class CouldNotOpenFileMessage
    {
        public FileInfo File { get; set; }
    }
}