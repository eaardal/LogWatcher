using System;
using System.Collections.ObjectModel;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;
using Application = System.Windows.Application;
using Message = LogWatcher.Infrastructure.Message;

namespace LogWatcher.ViewModels
{
    class LogDisplayViewModel<TLogEntry> : ViewModel where TLogEntry : BasicLogEntry
    {
        private string _lastChangeTime;
        
        public LogDisplayViewModel()
        {
            Message.Subscribe<NewLogEntryMessage<TLogEntry>>(OnNewLogEntry);

            LogEntries = new ObservableCollection<TLogEntry>();
        }

        public ObservableCollection<TLogEntry> LogEntries { get; private set; }
        public string EntryIdentifier { get; set; }

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

        protected virtual void OnNewLogEntry(NewLogEntryMessage<TLogEntry> message)
        {
            if (!String.IsNullOrEmpty(EntryIdentifier) && message.LogEntry.SourceIdentifier == EntryIdentifier)
            {
                LastChangeTime = DateTime.Now.ToLongTimeString();
                AddToLogOutput(message.LogEntry);
            }
        }

        private void AddToLogOutput(TLogEntry entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}