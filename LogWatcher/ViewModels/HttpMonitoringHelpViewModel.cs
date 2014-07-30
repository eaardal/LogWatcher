using LogWatcher.Domain;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class HttpMonitoringHelpViewModel : ViewModel
    {
        private string _serverUrl;
        private string _logEntryJson;
        private string _basicLogEntryJson;

        public HttpMonitoringHelpViewModel()
        {
            _serverUrl = Config.DefaultServerUrl;
            _logEntryJson = LogEntry.GetAsJsonFormat();
            _basicLogEntryJson = BasicLogEntry.GetAsJsonFormat();
        }

        public string ServerUrl
        {
            get { return _serverUrl; }
            set
            {
                if (value == _serverUrl) return;
                _serverUrl = value;
                NotifyPropertyChange();
            }
        }

        public string BasicLogEntryJson
        {
            get { return _basicLogEntryJson; }
            set
            {
                if (value == _basicLogEntryJson) return;
                _basicLogEntryJson = value;
                NotifyPropertyChange();
            }
        }

        public string LogEntryJson
        {
            get { return _logEntryJson; }
            set
            {
                if (value == _logEntryJson) return;
                _logEntryJson = value;
                NotifyPropertyChange();
            }
        }
    }
}
