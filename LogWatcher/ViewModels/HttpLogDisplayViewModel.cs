using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace LogWatcher.ViewModels
{
    class HttpLogDisplayViewModel : ViewModel
    {
        private string _lastChangeTime;
        public string EntryIdentifier { get; set; }

        public HttpLogDisplayViewModel()
        {
            Message.Subscribe<NewHttpLogEntryMessage>(OnNewHttpLogEntry);
            
            LogEntries = new ObservableCollection<object>();
        }

        public ObservableCollection<object> LogEntries { get; private set; }

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

        private void OnNewHttpLogEntry(NewHttpLogEntryMessage logEntry)
        {
            if (!String.IsNullOrEmpty(EntryIdentifier) && logEntry.HttpLogEntry.SourceApplication == EntryIdentifier)
            {
                LastChangeTime = DateTime.Now.ToLongTimeString();
                AddToLogOutput(logEntry.ToString());   
            }
        }

        private void AddToLogOutput(string entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}