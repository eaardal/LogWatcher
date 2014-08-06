using System.IO;

namespace LogWatcher.Domain.Messages.ErrorMessages
{
    internal class CouldNotOpenFileMessage
    {
        public FileInfo File { get; set; }
    }
}