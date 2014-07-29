using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogWatcher.Domain
{
    class FileLogService : ILogService
    {
        private FilePoller _filePoller;
        private readonly FileReader _fileReader;

        public FileLogService()
        {
             _fileReader = new FileReader();
        }

        public Action<LogEntry> NewLogEntryCallback { get; set; }
        public Action<FileInfo> FilePolledTickCallback { get; set; }
        
        public void StartPolling(string filepath)
        {
            var file = new FileInfo(filepath);
            if (file.Exists)
            {
                _filePoller = new FilePoller(file);
                _filePoller.FileHasChanges += async (source, f) => await FilePollerOnFileHasChanges(source, f);
                _filePoller.FilePollTick += FilePollerOnFilePollTick;
                _filePoller.Start();
            }
        }

        private void FilePollerOnFilePollTick(FilePoller source, FileInfo file)
        {
            if (FilePolledTickCallback != null)
                FilePolledTickCallback(file);
        }

        private async Task FilePollerOnFileHasChanges(FilePoller source, FileInfo file)
        {
            var changes = await _fileReader.ReadChanges(file);
            var newLines = changes.ToList();

            if (newLines.Any() && NewLogEntryCallback != null)
            {
                newLines.ForEach(line => NewLogEntryCallback(new LogEntry(DateTime.Now, file.Name, line)));
            }
        }
    }
}
