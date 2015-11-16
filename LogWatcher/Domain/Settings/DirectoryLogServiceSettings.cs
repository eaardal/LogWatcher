using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class DirectoryLogServiceSettings : NotifyPropertyChanged, ILogServiceSettings
    {
        private bool _filesChangedToday;
        private bool _lastChangedFile;
        private bool _filesChangedSinceTimestamp;
        private string _timestamp;
        private bool _shouldLogPollTicks;
        private int _pollInterval;

        public bool FilesChangedToday
        {
            get { return _filesChangedToday; }
            set
            {
                if (value == _filesChangedToday) return;
                _filesChangedToday = value;
                NotifyPropertyChange();
            }
        }

        public bool LastChangedFile
        {
            get { return _lastChangedFile; }
            set
            {
                if (value == _lastChangedFile) return;
                _lastChangedFile = value;
                NotifyPropertyChange();
            }
        }

        public bool FilesChangedSinceTimestamp
        {
            get { return _filesChangedSinceTimestamp; }
            set
            {
                if (value == _filesChangedSinceTimestamp) return;
                _filesChangedSinceTimestamp = value;
                NotifyPropertyChange();
            }
        }

        public string Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (value == _timestamp) return;
                _timestamp = value;
                NotifyPropertyChange();
            }
        }

        public bool ShouldLogPollTicks
        {
            get { return _shouldLogPollTicks; }
            set
            {
                if (value == _shouldLogPollTicks) return;
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
    }
}
