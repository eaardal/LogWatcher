using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LogWatcher.Views;

namespace LogWatcher.ViewModels
{
    abstract class MonitoringViewModelBase : ViewModel
    {
        private readonly Dictionary<LogDisplayViewModel, string> _logDisplays;

        protected MonitoringViewModelBase()
        {
            _logDisplays = new Dictionary<LogDisplayViewModel, string>();
            LogDisplayTabs = new ObservableCollection<TabItem>();
        }

        public ObservableCollection<TabItem> LogDisplayTabs { get; private set; }

        public void StartPolling(string filepath)
        {
            var viewModel = CreateNewLogDisplay(filepath);
            viewModel.StartPolling(filepath);
        }

        private LogDisplayViewModel CreateNewLogDisplay(string filepath)
        {
            var filename = GetFileName(filepath);
            var logDisplayView = new LogDisplayView();
            _logDisplays.Add(logDisplayView.ViewModel, filepath);
            LogDisplayTabs.Add(new TabItem { Header = filename, Content = logDisplayView, IsSelected = true});
            return logDisplayView.ViewModel;
        }

        private string GetFileName(string filepath)
        {
            var lastIndex = filepath.LastIndexOf("\\", System.StringComparison.Ordinal);
            var startindex = lastIndex + 1;
            var length = filepath.Length;
            return filepath.Substring(startindex, length - startindex);
        }
    }
}
