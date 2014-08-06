using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class FileLogServiceSettings : NotifyPropertyChanged
    {
        private bool _shouldLogFilePollTicks;
        private int _filePollInterval;
        private string _filePath;

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

        public int FilePollInterval
        {
            get { return _filePollInterval; }
            set
            {
                if (value == _filePollInterval) return;
                _filePollInterval = value;
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
