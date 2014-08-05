using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        private LogDisplaySettings _settings;
        private string _loadingScreenText;
        private Visibility _shouldShowLoadingScreen;

        public LogDisplayViewModel()
        {
            HideLoadingScreen();

            Message.Subscribe<NewLogEntryMessage<TLogEntry>>(OnNewLogEntry);
            Message.Subscribe<FilePollTickMessage>(OnFilePollTick);
            Message.Subscribe<ShowLoadingScreenMessage>(OnShowLoadingScreen);
            Message.Subscribe<HideLoadingScreenMessage>(OnHideLoadingScreen);
            Message.Subscribe<UpdateLoadingScreenTextMessage>(OnUpdateLoadingScreenText);

            LogEntries = new ObservableCollection<TLogEntry>();
            StatusMessages = new ObservableCollection<string>();
        }

        public ObservableCollection<TLogEntry> LogEntries { get; private set; }
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
            if (!String.IsNullOrEmpty(EntryIdentifier) && message.LogEntry.SourceIdentifier == EntryIdentifier)
            {
                if (Settings.ShouldLogFileChange)
                    AddStatusMessage("Change detecetd");

                AddToLogOutput(message.LogEntry);
            }
        }

        private void OnHideLoadingScreen(HideLoadingScreenMessage message)
        {
            if (message.Identifier == EntryIdentifier)
                HideLoadingScreen();
        }

        private void OnShowLoadingScreen(ShowLoadingScreenMessage message)
        {
            if (message.Identifier == EntryIdentifier)
                ShowLoadingScreen(message.Message);
        }

        private void OnFilePollTick(FilePollTickMessage message)
        {
            if (Settings.ShouldLogFilePollTicks)
                AddStatusMessage("Poll tick");
        }

        private void OnUpdateLoadingScreenText(UpdateLoadingScreenTextMessage message)
        {
            if (message.Identifier == EntryIdentifier)
                LoadingScreenText = message.Msg;
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

        private void AddStatusMessage(string msg)
        {
            StatusMessages.Insert(0, String.Format("{0}: {1}\t", DateTime.Now.ToLongTimeString(), msg));
        }

        private void AddToLogOutput(TLogEntry entry)
        {
            Application.Current.Dispatcher.Invoke((() => LogEntries.Insert(0, entry)));
        }
    }
}