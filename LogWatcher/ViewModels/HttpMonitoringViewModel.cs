using System;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.HttpInterface;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class HttpMonitoringViewModel : MonitoringViewModelBase
    {
        private readonly string _serverIsRunningTemplate = String.Format("Server is running @ ");
        private readonly string _serverIsStoppedTemplate = String.Format("Server is currently not running");
        private string _serverUrlDisplayText;

        public HttpMonitoringViewModel()
        {
            _serverUrlDisplayText = _serverIsRunningTemplate + Config.DefaultServerUrl; ;

            StartHttpServer();
            
            Message.Subscribe<ReceivedHttpLogEntryMessage<LogEntry>>(OnReceivedHttpLogEntry);
            Message.Subscribe<ReceivedHttpLogEntryMessage<BasicLogEntry>>(OnReceivedHttpBasicLogEntry);
        }

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

        private void StartHttpServer()
        {
            try
            {
                var http = new LogWatcherHttpServer();
                http.Connect(Config.DefaultServerUrl);
            }
            catch (Exception)
            {
                _serverUrlDisplayText = _serverIsStoppedTemplate;
            }
        }

        private void OnReceivedHttpBasicLogEntry(ReceivedHttpLogEntryMessage<BasicLogEntry> message)
        {
            if (ShouldCreateNewLogDisplay(message.LogEntry))
                CreateNewLogDisplay<BasicLogEntry>(message.LogEntry.SourceIdentifier);

            Message.Publish(new NewLogEntryMessage<BasicLogEntry> { LogEntry = message.LogEntry });
        }

        private void OnReceivedHttpLogEntry(ReceivedHttpLogEntryMessage<LogEntry> message)
        {
            if (ShouldCreateNewLogDisplay(message.LogEntry))
                CreateNewLogDisplay<LogEntry>(message.LogEntry.SourceIdentifier);

            Message.Publish(new NewLogEntryMessage<LogEntry> {LogEntry = message.LogEntry});
        }

        private bool ShouldCreateNewLogDisplay(BasicLogEntry logEntry)
        {
            return !LogDisplays.Contains(logEntry.SourceIdentifier);
        }
    }
}
