using System;
using System.Collections.ObjectModel;
using System.Windows;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class FileLogDisplayViewModel : ViewModel
    {
        private readonly FileLogService _fileLogService;
        private string _lastPollTime;
        private string _lastChangeTime;

        public FileLogDisplayViewModel()
        {
            _fileLogService = new FileLogService();
            LogEntries = new ObservableCollection<LogEntry>();

            Message.Subscribe<FilePollTickMessage>(OnFilePollTick);
            Message.Subscribe<NewLogEntryMessage>(OnNewLogEntry);
        }

        public ObservableCollection<LogEntry> LogEntries { get; private set; }

        public string EntryIdentifier { get; set; }

        public string LastPollTime
        {
            get { return _lastPollTime; }
            private set
            {
                if (value == _lastPollTime) return;
                _lastPollTime = value;
                NotifyPropertyChange();
            }
        }

        public string LastChangeTime
        {
            get { return _lastChangeTime; }
            private set
            {
                if (value == _lastChangeTime) return;
                _lastChangeTime = value;
                NotifyPropertyChange();
            }
        }

        public void StartPolling(string filepath)
        {
            _fileLogService.StartProcessing(filepath);
        }

        private void OnNewLogEntry(NewLogEntryMessage message)
        {
            if (EntryIdentifier == message.LogEntry.SourceIdentifier)
            {
                LastChangeTime = DateTime.Now.ToLongTimeString();
                AddToLogOutput(message.LogEntry);    
            }
        }

        private void OnFilePollTick(FilePollTickMessage obj)
        {
            LastPollTime = DateTime.Now.ToLongTimeString();
        }
        
        private void AddToLogOutput(LogEntry entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}
