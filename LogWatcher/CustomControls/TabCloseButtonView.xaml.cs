using System;
using System.Windows;

namespace LogWatcher.CustomControls
{
    public partial class TabCloseButtonView
    {
        public event EventHandler Click;
 
        public TabCloseButtonView()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(sender, e);
            }
        }
    }
}
