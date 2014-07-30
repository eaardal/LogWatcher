using LogWatcher.Domain;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class HttpMonitoringHelpViewModel : ViewModel
    {
        private string _serverUrl;
        private string _httpLogEntryJson;

        public HttpMonitoringHelpViewModel()
        {
            _serverUrl = Config.DefaultServerUrl;
            _httpLogEntryJson = LogEntry.GetAsJsonFormat();
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

        public string HttpLogEntryJson
        {
            get { return _httpLogEntryJson; }
            set
            {
                if (value == _httpLogEntryJson) return;
                _httpLogEntryJson = value;
                NotifyPropertyChange();
            }
        }
    }
}
