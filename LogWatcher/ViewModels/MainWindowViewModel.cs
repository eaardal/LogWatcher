using System.Windows;
using LogWatcher.Domain.Messages;
using LogWatcher.Domain.Messages.ErrorMessages;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _messageText;
        private Visibility _mainLayoutVisibility;
        private Visibility _messageOverlayVisibility;

        public MainWindowViewModel()
        {
            MainLayoutVisibility = Visibility.Visible;
            MessageOverlayVisibility = Visibility.Hidden;

            Message.Subscribe<FileNotFoundMessage>(msg => ShowMessageOverlay("Could not find file " + msg.File.FullName));
            Message.Subscribe<GenericExceptionMessage>(msg =>
            {
                ShowMessageOverlay("An error occurred: " + msg.Exception.Message);
                MessageBox.Show(msg.Exception.StackTrace);
            });
            Message.Subscribe<CouldNotOpenFileMessage>(msg => ShowMessageOverlay("Could not open file " + msg.File.FullName));
        }

        public string MessageText
        {
            get { return _messageText; }
            set
            {
                if (value == _messageText) return;
                _messageText = value;
                NotifyPropertyChange();
            }
        }

        public Visibility MainLayoutVisibility
        {
            get { return _mainLayoutVisibility; }
            set
            {
                if (value == _mainLayoutVisibility) return;
                _mainLayoutVisibility = value;
                NotifyPropertyChange();
            }
        }

        public Visibility MessageOverlayVisibility
        {
            get { return _messageOverlayVisibility; }
            set
            {
                if (value == _messageOverlayVisibility) return;
                _messageOverlayVisibility = value;
                NotifyPropertyChange();
            }
        }

        internal void HideMessageOverlay()
        {
            MessageOverlayVisibility = Visibility.Hidden;
        }

        private void ShowMessageOverlay(string message)
        {
            MessageText = message;
            MessageOverlayVisibility = Visibility.Visible;
        }
    }
}
