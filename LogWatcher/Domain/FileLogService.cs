using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain
{
    class FileLogService : ILogService
    {
        private FilePoller _filePoller;
        private readonly FileReader _fileReader;

        public FileLogService()
        {
            _fileReader = new FileReader();

            Message.Subscribe<FileChangeDetectedMessage>(async msg => await OnFileChangeDetected(msg));
            Message.Subscribe<FileNotFoundMessage>(OnFileNotFound);
        }

        private void OnFileNotFound(FileNotFoundMessage message)
        {
            _filePoller.Stop();
        }

        private async Task OnFileChangeDetected(FileChangeDetectedMessage message)
        {
            var newLines = await _fileReader.ReadChanges(message.File);
            newLines.ToList().ForEach(line => Message.Publish(new NewLogEntryMessage<BasicLogEntry>
            {
                LogEntry = BasicLogEntry.Parse(new BasicTextFormat(), message.File.FullName, line)
            }));
        }

        public void StartProcessing(params string[] parameters)
        {
            if (!VerifyHasRequiredParameters(parameters)) return;

            var filePath = parameters[0];
            var pollInterval = Convert.ToInt32(parameters[1]);

            var file = new FileInfo(filePath);
            if (file.Exists)
            {
                _filePoller = new FilePoller(file, pollInterval);
                _filePoller.Start();
            }
        }

        private bool VerifyHasRequiredParameters(string[] parameters)
        {
            var filepath = parameters[0];
            var pollInterval = parameters[1];

            var isValid = !String.IsNullOrEmpty(filepath) && File.Exists(filepath);
            if (!isValid)
            {
                Message.Publish(new FileNotFoundMessage { File = new FileInfo(filepath) });
            }

            int result;
            isValid = !String.IsNullOrEmpty(pollInterval) && Int32.TryParse(pollInterval, out result) && result > 500;
            
            if (!isValid)
                Message.Publish(new PollIntervalNotValidMessage { PollInterval = pollInterval, Sender = this});

            return isValid;
        }
    }
}
