using LogWatcher.Domain.Settings;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class DirectoryMonitoringSettingsViewModel : ViewModel
    {
        private DirectoryMonitoringSettings _settings;

        public DirectoryMonitoringSettingsViewModel()
        {
            _settings = new DirectoryMonitoringSettings();
        }

        public DirectoryMonitoringSettings Settings
        {
            get { return _settings; }
            set
            {
                if (Equals(value, _settings)) return;
                _settings = value;
                NotifyPropertyChange();
            }
        }
    }
}
