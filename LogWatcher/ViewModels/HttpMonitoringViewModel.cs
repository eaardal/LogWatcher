using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LogWatcher.HttpInterface;
using LogWatcher.Views;

namespace LogWatcher.ViewModels
{
    class HttpMonitoringViewModel : ViewModel
    {
        private readonly string _serverIsRunningTemplate = String.Format("Server is running @ ");
        private readonly string _serverIsStoppedTemplate = String.Format("Server is currently not running");
        private string _serverUrlDisplayText;
        private readonly Dictionary<string, HttpLogDisplayViewModel> _logDisplays;

        public HttpMonitoringViewModel()
        {
            Message.Subscribe<ReceivedHttpLogEntryMessage>(OnReceivedHttpLogEntry);

            _serverUrlDisplayText = _serverIsRunningTemplate + Config.DefaultServerUrl; ;
            _logDisplays = new Dictionary<string, HttpLogDisplayViewModel>();
            LogDisplayTabs = new ObservableCollection<TabItem>();

            var http = new LogWatcherHttpServer();
            http.Connect(Config.DefaultServerUrl);
        }

        public ObservableCollection<TabItem> LogDisplayTabs { get; private set; }

        public string ServerUrlDisplayText
        {
            get { return _serverUrlDisplayText; }
            set
            {
                if (value == _serverUrlDisplayText) return;
                _serverUrlDisplayText = value;
                NotifyPropertyChange();
            }
        }

        private HttpLogDisplayViewModel CreateNewLogDisplay(string identifier)
        {
            var logDisplayView = new HttpLogDisplayView();
            var viewModel = logDisplayView.ViewModel;
            viewModel.EntryIdentifier = identifier;
            _logDisplays.Add(identifier, logDisplayView.ViewModel);
            LogDisplayTabs.Add(new TabItem { Header = identifier, Content = logDisplayView, IsSelected = true });
            return logDisplayView.ViewModel;
        }

        private void OnReceivedHttpLogEntry(ReceivedHttpLogEntryMessage message)
        {
            if (!_logDisplays.ContainsKey(message.HttpLogEntry.SourceApplication))
                CreateNewLogDisplay(message.HttpLogEntry.SourceApplication);

            Message.Publish(new NewHttpLogEntryMessage {HttpLogEntry = message.HttpLogEntry});
        }
    }
}
