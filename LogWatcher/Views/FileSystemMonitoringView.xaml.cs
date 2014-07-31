using System.Windows;
using System.Windows.Forms;
using LogWatcher.Domain;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class FileSystemMonitoringView
    {
        private readonly FileSystemMonitoringViewModel _viewModel;

        public FileSystemMonitoringView()
        {
            _viewModel = new FileSystemMonitoringViewModel(new FileLogService());
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void BtnStartPolling_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.StartPolling();
        }

        private void BtnBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenFileDialog();
        }
    }
}
