using System.Windows;
using System.Windows.Forms;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly string _mockFilePath = @"C:\Users\eiard\Documents\Visual Studio 2012\Projects\LogWatcher\MockFilesystem\dir\textfile.txt";

        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();
            InitializeComponent();
            DataContext = _viewModel;
            TxtFilePath.Text = _mockFilePath;
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
