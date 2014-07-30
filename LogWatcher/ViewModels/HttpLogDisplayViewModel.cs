using System;
using System.Collections.ObjectModel;
using System.Windows;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class HttpLogDisplayViewModel : ViewModel
    {
        private string _lastChangeTime;
        public string EntryIdentifier { get; set; }

        public HttpLogDisplayViewModel()
        {
            Message.Subscribe<NewLogEntryMessage>(OnNewLogEntry);
            
            LogEntries = new ObservableCollection<LogEntry>();
        }

        public ObservableCollection<LogEntry> LogEntries { get; private set; }

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

        private void OnNewLogEntry(NewLogEntryMessage message)
        {
            if (!String.IsNullOrEmpty(EntryIdentifier) && message.LogEntry.SourceIdentifier == EntryIdentifier)
            {
                LastChangeTime = DateTime.Now.ToLongTimeString();
                AddToLogOutput(message.LogEntry);   
            }
        }

        private void AddToLogOutput(LogEntry entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}