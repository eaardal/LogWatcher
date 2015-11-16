using System.Windows;
using System.Windows.Controls;
using LogWatcher.Domain;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class DirectoryMonitoringView
    {
        private readonly DirectoryMonitoringViewModel _viewModel;

        public DirectoryMonitoringView()
        {
            _viewModel = new DirectoryMonitoringViewModel(new FileLogService());
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

        private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
        {
            new DirectoryMonitoringSettingsView(_viewModel.Settings).Show();
        }
    }
}
