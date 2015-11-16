using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogWatcher.Domain.Messages;
using LogWatcher.Domain.Messages.ErrorMessages;
using LogWatcher.Domain.Settings;
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
            try
            {
                var identifier = message.File.FullName;
                var fileChangeInfo = await _fileReader.ReadChanges(message.FileBytes, identifier);

                var logEntries = fileChangeInfo.ChangedLines.Select(line => BasicLogEntry.Parse(new BasicTextFormat(), identifier, line.Value, line.Key));

                Message.Publish(new NewLogEntriesMessage<BasicLogEntry>(identifier) { LogEntries = logEntries });
            }
            catch (Exception ex)
            {
                Message.Publish(new GenericExceptionMessage(ex));
            }
        }

        public void StartProcessing(ILogServiceSettings settings)
        {
            var fileSettings = (FileLogServiceSettings) settings;
            var file = new FileInfo(fileSettings.FilePath);

            if (!file.Exists) return;

            _filePoller = new FilePoller(file, settings.PollInterval) { ShouldLogPollTicks = settings.ShouldLogPollTicks };
            _filePoller.Start();

            SubscribeToSettingsChanges(fileSettings);
        }

        private void SubscribeToSettingsChanges(FileLogServiceSettings settings)
        {
            settings.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ShouldLogPollTicks")
                    _filePoller.ShouldLogPollTicks = settings.ShouldLogPollTicks;
            };
        }
    }
}
