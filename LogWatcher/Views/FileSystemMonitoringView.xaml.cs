using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class FileSystemMonitoringView
    {
        private readonly FileSystemMonitoringViewModel _viewModel;

        public FileSystemMonitoringView()
        {
            _viewModel = new FileSystemMonitoringViewModel();
            InitializeComponent();
            DataContext = _viewModel;
            TxtFilePath.Text = GetTestFilePath();
        }

        private string GetTestFilePath()
        {
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            return currentLocation.Substring(0, currentLocation.LastIndexOf('\\')) + "\\Testfile.txt";
        }

        private void BtnStartPolling_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.StartPolling(GetFilePath());
        }

        private string GetFilePath()
        {
            return TxtFilePath.Text;
        }

        private void BtnBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TxtFilePath.Text = dialog.FileName;
            }
        }
    }
}
