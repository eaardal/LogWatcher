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

        private void CopyGetUrlToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.ServerUrl);
        }

        private void CopyPostDefaultJsonToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.BasicLogEntryJson);
        }

        private void CopyPostDefaultUrlToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.ServerUrl);
        }

        private void CopyPostFullUrlToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.ServerUrl + "full");
        }

        private void CopyPostFullJsonToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_viewModel.LogEntryJson);
        }
    }
}
