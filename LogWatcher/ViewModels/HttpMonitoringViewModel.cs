using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LogWatcher.Domain;
using LogWatcher.HttpInterface;
using LogWatcher.Views;

namespace LogWatcher.ViewModels
{
    class HttpMonitoringViewModel : ViewModel
    {
        private readonly string _serverIsRunningTemplate = String.Format("Server is running @ ");
        private readonly string _serverIsStoppedTemplate = String.Format("Server is currently not running");
        private string _serverUrlDisplayText;
        private readonly Dictionary<LogDisplayViewModel, string> _logDisplays;

        private readonly HttpLogService _httpLogService;
        
        public HttpMonitoringViewModel()
        {
            _serverUrlDisplayText = _serverIsRunningTemplate + Config.DefaultServerUrl; ;
            _httpLogService = new HttpLogService();
            _httpLogService.NewLogEntryCallback += OnNewLogEntry;
            _httpLogService.StartProcessing();    
            _logDisplays = new Dictionary<LogDisplayViewModel, string>();
            LogDisplayTabs = new ObservableCollection<TabItem>();
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

        private LogDisplayViewModel CreateNewLogDisplay(string identifier)
        {
            var logDisplayView = new LogDisplayView();
            _logDisplays.Add(logDisplayView.ViewModel, identifier);
            LogDisplayTabs.Add(new TabItem { Header = identifier, Content = logDisplayView, IsSelected = true });
            return logDisplayView.ViewModel;
        }

        private string GetFileName(string filepath)
        {
            var lastIndex = filepath.LastIndexOf("\\", System.StringComparison.Ordinal);
            var startindex = lastIndex + 1;
            var length = filepath.Length;
            return filepath.Substring(startindex, length - startindex);
        }
        
        private void OnNewLogEntry(HttpLogEntry logEntry)
        {
            var viewModel = CreateNewLogDisplay(logEntry.SourceApplication);
            viewModel.StartPolling("");
        }

       
    }
}
