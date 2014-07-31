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
            TxtFilePath.Text = _viewModel.GetTestFilePath();
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
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = _viewModel.GetExecutingPath()
            };

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtFilePath.Text = dialog.FileName;
                _viewModel.StartPolling(GetFilePath());
            }
        }
    }
}
