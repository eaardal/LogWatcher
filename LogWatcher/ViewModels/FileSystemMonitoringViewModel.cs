using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LogWatcher.Views;

namespace LogWatcher.ViewModels
{
    class FileSystemMonitoringViewModel
    {
        private readonly Dictionary<string, FileLogDisplayViewModel> _logDisplays;

        public FileSystemMonitoringViewModel()
        {
            _logDisplays = new Dictionary<string, FileLogDisplayViewModel>();
            LogDisplayTabs = new ObservableCollection<TabItem>();
        }

        public ObservableCollection<TabItem> LogDisplayTabs { get; private set; }

        public void StartPolling(string filepath)
        {
            if (!_logDisplays.ContainsKey(filepath))
            {
                var viewModel = CreateNewLogDisplay(filepath);
                viewModel.StartPolling(filepath);    
            }
        }

        private FileLogDisplayViewModel CreateNewLogDisplay(string filepath)
        {
            var filename = GetFileName(filepath);
            var logDisplayView = new FileLogDisplayView();
            var vm = logDisplayView.ViewModel;
            vm.EntryIdentifier = filepath;
            _logDisplays.Add(filepath, vm);
            LogDisplayTabs.Add(new TabItem { Header = filename, Content = logDisplayView, IsSelected = true });
            return vm;
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
