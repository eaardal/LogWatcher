using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class LogDisplaySettings : NotifyPropertyChanged
    {
        private bool _shouldLogFilePollTicks;
        private bool _shouldLogFileChange;

        public bool ShouldLogFilePollTicks
        {
            get { return _shouldLogFilePollTicks; }
            set
            {
                if (value.Equals(_shouldLogFilePollTicks)) return;
                _shouldLogFilePollTicks = value;
                NotifyPropertyChange();
            }
        }

        public bool ShouldLogFileChange
        {
            get { return _shouldLogFileChange; }
            set
            {
                if (value.Equals(_shouldLogFileChange)) return;
                _shouldLogFileChange = value;
                NotifyPropertyChange();
            }
        }
    }
}
