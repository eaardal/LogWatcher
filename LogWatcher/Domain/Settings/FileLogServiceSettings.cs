using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class FileLogServiceSettings : NotifyPropertyChanged, ILogServiceSettings
    {
        private bool _shouldLogPollTicks;
        private int _pollInterval;
        private string _filePath;

        public bool ShouldLogPollTicks
        {
            get { return _shouldLogPollTicks; }
            set
            {
                if (value.Equals(_shouldLogPollTicks)) return;
                _shouldLogPollTicks = value;
                NotifyPropertyChange();
            }
        }

        public int PollInterval
        {
            get { return _pollInterval; }
            set
            {
                if (value == _pollInterval) return;
                _pollInterval = value;
                NotifyPropertyChange();
            }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value == _filePath) return;
                _filePath = value;
                NotifyPropertyChange();
            }
        }
    }
}
