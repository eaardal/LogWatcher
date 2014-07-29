using System.Reflection;
using System.Windows;
using System.Windows.Forms;
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
    }
}
