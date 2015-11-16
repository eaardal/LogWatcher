using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain.Settings
{
    class DirectoryMonitoringSettings : NotifyPropertyChanged
    {
        private bool _filesChangedToday;
        private bool _lastChangedFile;
        private bool _filesChangedSinceTimestamp;
        private string _timestamp;
        private bool _shouldLogFileChange;

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

        public bool ShouldLogFileChange
        {
            get { return _shouldLogFileChange; }
            set
            {
                if (value == _shouldLogFileChange) return;
                _shouldLogFileChange = value;
                NotifyPropertyChange();
            }
        }
    }
}
