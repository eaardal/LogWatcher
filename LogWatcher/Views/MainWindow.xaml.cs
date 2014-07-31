using System.Windows;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _viewModel;
        
        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void CloseMessageOverlayClick(object sender, RoutedEventArgs e)
        {
            _viewModel.HideMessageOverlay();
        }
    }
}
