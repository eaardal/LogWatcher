using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using LogWatcher.Domain;

namespace LogWatcher.ViewModels
{
    class LogDisplayViewModel : ViewModel
    {
        private readonly FileLogService _fileLogService;
        private string _lastPollTime;
        private string _lastChangeTime;

        public LogDisplayViewModel()
        {
            _fileLogService = new FileLogService();
            _fileLogService.NewLogEntryCallback = OnLogEntryReceived;
            _fileLogService.FilePolledTickCallback = OnFilePolledTick;

            LogEntries = new ObservableCollection<object>();
        }

        private void OnFilePolledTick(FileInfo obj)
        {
            LastPollTime = DateTime.Now.ToLongTimeString();
        }

        public ObservableCollection<object> LogEntries { get; private set; }

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
            _fileLogService.StartPolling(filepath);
        }

        private void OnLogEntryReceived(LogEntry logEntry)
        {
            LastChangeTime = DateTime.Now.ToLongTimeString();
            AddToLogOutput(logEntry.ToString());
        }

        private void AddToLogOutput(string entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}
