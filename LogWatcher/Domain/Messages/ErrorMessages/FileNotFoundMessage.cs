using System.IO;

namespace LogWatcher.Domain.Messages.ErrorMessages
{
    internal class FileNotFoundMessage
    {
        public FileInfo File { get; set; }
    }
}