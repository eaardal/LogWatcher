using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LogWatcher.Domain;
using LogWatcher.Infrastructure;
using LogWatcher.Views;

namespace LogWatcher.ViewModels
{
    abstract class MonitoringViewModelBase : ViewModel
    {
        protected MonitoringViewModelBase()
        {
            LogDisplays = new List<string>();
            LogDisplayTabs = new ObservableCollection<TabItem>();
        }

        protected List<string> LogDisplays { get; private set; }
        public ObservableCollection<TabItem> LogDisplayTabs { get; private set; }

        protected void CreateNewLogDisplay<TLogEntry>(string identifier, string displayTitle) where TLogEntry : BasicLogEntry
        {
            if (!LogDisplays.Contains(identifier))
            {
                var viewModel = new LogDisplayViewModel<TLogEntry> {EntryIdentifier = identifier};
                var view = new LogDisplayView();
                view.SetViewModel(viewModel);
                
                LogDisplayTabs.Add(new TabItem
                {
                    Header = displayTitle,
                    Content = view,
                    IsSelected = true
                });

                LogDisplays.Add(identifier);
            }
        }

        protected void CreateNewLogDisplay<TLogEntry>(string identifier) where TLogEntry : BasicLogEntry
        {
            CreateNewLogDisplay<TLogEntry>(identifier, identifier);
        }
    }
}
