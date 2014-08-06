using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Domain.Settings;
using LogWatcher.Infrastructure;
using Application = System.Windows.Application;
using Message = LogWatcher.Infrastructure.Message;

namespace LogWatcher.ViewModels
{
    class LogDisplayViewModel<TLogEntry> : ViewModel where TLogEntry : BasicLogEntry
    {
        private ObservableCollection<string> _statusMessages;
        private Dictionary<long, string> _statusMessageCache; 
        private LogDisplaySettings _settings;
        private string _loadingScreenText;
        private Visibility _shouldShowLoadingScreen;
        private ObservableCollection<TLogEntry> _logEntries;

        public LogDisplayViewModel()
        {
            HideLoadingScreen();

            Message.Subscribe<NewLogEntryMessage<TLogEntry>>(OnNewLogEntry);
            Message.Subscribe<NewLogEntriesMessage<TLogEntry>>(OnNewLogEntries);
            Message.Subscribe<StatusBarMessage>(OnStatusBarMessage);
            Message.Subscribe<ShowLoadingScreenMessage>(OnShowLoadingScreen);
            Message.Subscribe<HideLoadingScreenMessage>(OnHideLoadingScreen);
            Message.Subscribe<UpdateLoadingScreenTextMessage>(OnUpdateLoadingScreenText);

            LogEntries = new ObservableCollection<TLogEntry>();
            StatusMessages = new ObservableCollection<string>();
            _statusMessageCache = new Dictionary<long, string>();
        }
        
        public ObservableCollection<TLogEntry> LogEntries
        {
            get { return _logEntries; }
            private set
            {
                if (Equals(value, _logEntries)) return;
                _logEntries = value;
                NotifyPropertyChange();
            }
        }

        public string EntryIdentifier { get; set; }

        public ObservableCollection<string> StatusMessages
        {
            get { return _statusMessages; }
            set
            {
                if (Equals(value, _statusMessages)) return;
                _statusMessages = value;
                NotifyPropertyChange();
            }
        }

        public LogDisplaySettings Settings
        {
            get { return _settings; }
            set
            {
                if (Equals(value, _settings)) return;
                _settings = value;
                NotifyPropertyChange();
            }
        }

        public Visibility ShouldShowLoadingScreen
        {
            get { return _shouldShowLoadingScreen; }
            set
            {
                if (value == _shouldShowLoadingScreen) return;
                _shouldShowLoadingScreen = value;
                NotifyPropertyChange();
            }
        }

        public string LoadingScreenText
        {
            get { return _loadingScreenText; }
            set
            {
                if (value == _loadingScreenText) return;
                _loadingScreenText = value;
                NotifyPropertyChange();
            }
        }

        protected virtual void OnNewLogEntry(NewLogEntryMessage<TLogEntry> message)
        {
            if (IsLogDisplayForLogObject(message.LogEntry.SourceIdentifier))
            {
                if (Settings.ShouldLogFileChange)
                    AddStatusMessage("Change detected", message.TimestampTicks);

                AddToLogOutput(message.LogEntry);
            }
        }

        private void OnHideLoadingScreen(HideLoadingScreenMessage message)
        {
            if (IsLogDisplayForLogObject(message.Identifier))
                HideLoadingScreen();
        }

        private void OnShowLoadingScreen(ShowLoadingScreenMessage message)
        {
            if (IsLogDisplayForLogObject(message.Identifier))
                ShowLoadingScreen(message.Message);
        }

        private void OnStatusBarMessage(StatusBarMessage message)
        {
            if (IsLogDisplayForLogObject(message.Identifier))
                AddStatusMessage(message.Text, message.TimestampTicks);
        }

        private void OnUpdateLoadingScreenText(UpdateLoadingScreenTextMessage message)
        {
            if (IsLogDisplayForLogObject(message.Identifier))
                LoadingScreenText = message.Msg;
        }

        private void OnNewLogEntries(NewLogEntriesMessage<TLogEntry> message)
        {
            if (IsLogDisplayForLogObject(message.Identifier))
            {
                if (Settings.ShouldLogFileChange)
                    AddStatusMessage("Change detecetd", message.TimestampTicks);
                
                AddToLogOutput(message.LogEntries);
            }
        }

        private void ShowLoadingScreen(string msg)
        {
            LoadingScreenText = msg;
            ShouldShowLoadingScreen = Visibility.Visible;
        }

        private void HideLoadingScreen()
        {
            ShouldShowLoadingScreen = Visibility.Hidden;
        }

        private void AddStatusMessage(string msg, long timestampTicks)
        {
            _statusMessageCache.Add(timestampTicks, String.Format("{0}: {1}\t", DateTime.Now.ToLongTimeString(), msg));
            var orderedMessages = _statusMessageCache.OrderByDescending(x => x.Key).Select(x => x.Value).ToList();
            StatusMessages = new ObservableCollection<string>(orderedMessages);
        }

        private void AddToLogOutput(IEnumerable<TLogEntry> entries)
        {
            var ordered = entries.Concat(LogEntries).OrderByDescending(entry => entry.LineNr);
            Application.Current.Dispatcher.Invoke((() => LogEntries = new ObservableCollection<TLogEntry>(ordered)));
        }

        private void AddToLogOutput(TLogEntry entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }

        private bool IsLogDisplayForLogObject(string identifierToVerify)
        {
            return !String.IsNullOrEmpty(EntryIdentifier) && identifierToVerify == EntryIdentifier;
        }
    }
}