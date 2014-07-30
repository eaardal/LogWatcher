using System.Windows;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class HttpMonitoringView
    {
        public HttpMonitoringView()
        {
            InitializeComponent();
            DataContext = new HttpMonitoringViewModel();
        }

        private void BtnOpenHelp_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new HttpMonitoringHelpView();
            view.Show();
        }
    }
}
