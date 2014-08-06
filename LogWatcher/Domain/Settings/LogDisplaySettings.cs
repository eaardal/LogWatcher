using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class LogDisplaySettings : NotifyPropertyChanged
    {
        private bool _shouldLogFileChange;

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
