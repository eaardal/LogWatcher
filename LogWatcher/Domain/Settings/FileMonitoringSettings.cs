using System;
using System.Globalization;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class FileMonitoringSettings : NotifyPropertyChanged
    {
        private bool _shouldShouldLogFilePollTicks;
        private bool _shouldLogFileChange;
        private string _interval;

        public FileMonitoringSettings()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Interval = Config.DefaultPollInterval.ToString(CultureInfo.InvariantCulture);
            ShouldLogFileChange = true;
            ShouldLogFilePollTicks = false;
        }

        public bool ShouldLogFilePollTicks
        {
            get { return _shouldShouldLogFilePollTicks; }
            set
            {
                if (value.Equals(_shouldShouldLogFilePollTicks)) return;
                _shouldShouldLogFilePollTicks = value;
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

        public string Interval
        {
            get { return _interval; }
            set
            {
                int result;
                if (value == _interval || !Int32.TryParse(value, out result)) return;
                _interval = value;
                NotifyPropertyChange();
            }
        }
    }
}
