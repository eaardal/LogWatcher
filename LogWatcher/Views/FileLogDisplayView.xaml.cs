using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class FileLogDisplayView
    {
        internal FileLogDisplayViewModel ViewModel { get; private set; }

        public FileLogDisplayView()
        {
            ViewModel = new FileLogDisplayViewModel();
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
