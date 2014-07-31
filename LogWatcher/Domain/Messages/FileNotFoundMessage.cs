using System.IO;

namespace LogWatcher.Domain.Messages
{
    internal class FileNotFoundMessage
    {
        public FileInfo File { get; set; }
    }
}