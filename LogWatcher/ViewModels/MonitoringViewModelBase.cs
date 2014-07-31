using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using LogWatcher.CustomControls;
using LogWatcher.CustomControls.Messages;
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

            Message.Subscribe<TabItemClosedMessage>(OnTabItemClosed);
        }

        private void OnTabItemClosed(TabItemClosedMessage message)
        {
            var identifier = message.TabItem.Tag.ToString();
            LogDisplays.Remove(identifier);
            LogDisplayTabs.Remove(message.TabItem);
        }

        protected List<string> LogDisplays { get; private set; }
        public ObservableCollection<TabItem> LogDisplayTabs { get; private set; }

        protected bool ShouldCreateNewLogDisplay(string identifier)
        {
            return !LogDisplays.Contains(identifier);
        }

        protected void CreateNewLogDisplay<TLogEntry>(string identifier, string displayTitle) where TLogEntry : BasicLogEntry
        {
            if (ShouldCreateNewLogDisplay(identifier))
            {
                var viewModel = new LogDisplayViewModel<TLogEntry> {EntryIdentifier = identifier};
                var view = new LogDisplayView();
                view.SetViewModel(viewModel);
                
                var tabItem = new CloseableTabItem
                {
                    Content = view,
                    IsSelected = true,
                    Tag = identifier
                };

                tabItem.SetHeader(new TextBlock {Text = displayTitle});
                LogDisplayTabs.Add(tabItem);

                LogDisplays.Add(identifier);
            }
        }

        protected void CreateNewLogDisplay<TLogEntry>(string identifier) where TLogEntry : BasicLogEntry
        {
            CreateNewLogDisplay<TLogEntry>(identifier, identifier);
        }
    }
}
