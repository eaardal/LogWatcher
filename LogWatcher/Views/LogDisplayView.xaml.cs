using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class LogDisplayView
    {
        internal LogDisplayViewModel ViewModel { get; private set; }

        public LogDisplayView()
        {
            ViewModel = new LogDisplayViewModel();
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
