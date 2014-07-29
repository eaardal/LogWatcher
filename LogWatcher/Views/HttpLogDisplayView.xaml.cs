using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    public partial class HttpLogDisplayView
    {
        internal HttpLogDisplayViewModel ViewModel { get; private set; }

        public HttpLogDisplayView()
        {
            ViewModel = new HttpLogDisplayViewModel();
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
