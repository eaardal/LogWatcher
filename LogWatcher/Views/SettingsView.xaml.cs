using System.Windows;
using LogWatcher.Domain.Settings;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class SettingsView
    {
        private readonly SettingsViewModel _viewModel;

        public SettingsView()
        {
            _viewModel = new SettingsViewModel();
            DataContext = _viewModel;
            InitializeComponent();
        }

        internal SettingsView(FileMonitoringSettings settings) : this()
        {
            _viewModel.Settings = settings;
        }

        private void SaveSettings_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
