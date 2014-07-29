using System.Windows;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class HttpMonitoringView
    {
        private readonly HttpMonitoringViewModel _viewModel;

        public HttpMonitoringView()
        {
            _viewModel = new HttpMonitoringViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
