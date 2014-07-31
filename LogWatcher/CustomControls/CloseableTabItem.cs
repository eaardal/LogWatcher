using System.Windows;
using System.Windows.Controls;
using LogWatcher.CustomControls.Messages;
using LogWatcher.Infrastructure;
using LogWatcher.Views;

namespace LogWatcher.CustomControls
{
    //
    //  Thanks to Adam Prescott!
    //  http://adamprescott.net/2012/09/05/closeable-tabs-in-wpf-made-easy/
    //

    public class CloseableTabItem : TabItem
    {
        public void SetHeader(UIElement header)
        {
            // Container for header controls
            var dockPanel = new DockPanel();
            dockPanel.Children.Add(header);

            // Close button to remove the tab
            var closeButton = new TabCloseButtonView();
            closeButton.Click += (sender, e) => Message.Publish(new TabItemClosedMessage { TabItem = this });

            dockPanel.Children.Add(closeButton);

            // Set the header
            Header = dockPanel;
        }
    }
}
