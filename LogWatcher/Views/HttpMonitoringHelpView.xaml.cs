using System.Windows;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class HttpMonitoringHelpView
    {
        private readonly HttpMonitoringHelpViewModel _viewModel;

        public HttpMonitoringHelpView()
        {
            _viewModel = new HttpMonitoringHelpViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void BtnCopyJson_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.HttpLogEntryJson);
        }

        private void BtnCopyServerUrl_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.ServerUrl);
        }
    }
}
