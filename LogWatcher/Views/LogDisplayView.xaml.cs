using LogWatcher.Domain;
using LogWatcher.ViewModels;

namespace LogWatcher.Views
{
    partial class LogDisplayView
    {
        public LogDisplayView()
        {
            InitializeComponent();
        }

        internal void SetViewModel<TLogEntry>(LogDisplayViewModel<TLogEntry> viewModel) where TLogEntry : BasicLogEntry
        {
            DataContext = viewModel;
        }
    }
}
