using LogWatcher.Domain.Settings;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class SettingsViewModel : ViewModel
    {
        private FileMonitoringSettings _settings;

        public SettingsViewModel()
        {
            _settings = new FileMonitoringSettings();
        }

        public FileMonitoringSettings Settings
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
