using System.Windows;
using System.Windows.Controls;
using LogWatcher.Domain.Settings;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class DirectoryMonitoringSettingsView
    {
        private readonly DirectoryMonitoringSettingsViewModel _viewModel;

        public DirectoryMonitoringSettingsView()
        {
            _viewModel = new DirectoryMonitoringSettingsViewModel();
            DataContext = _viewModel;
            InitializeComponent();
        }

        internal DirectoryMonitoringSettingsView(DirectoryMonitoringSettings settings) : this()
        {
            _viewModel.Settings = settings;
        }

        private void SaveSettings_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
