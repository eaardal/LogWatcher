using System;
using System.Windows.Forms;
using LogWatcher.Annotations;
using LogWatcher.Domain;
using LogWatcher.Domain.Helpers;
using LogWatcher.Domain.Settings;

namespace LogWatcher.ViewModels
{
    class FileSystemMonitoringViewModel : MonitoringViewModelBase
    {
        private readonly ILogService _logService;
        private string _filePath;
        private FileMonitoringSettings _settings;

        public FileSystemMonitoringViewModel([NotNull] ILogService logService)
        {
            if (logService == null) throw new ArgumentNullException("logService");
            _logService = logService;
            _settings = new FileMonitoringSettings();

            SetDefaultValues();
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

        public void StartPolling()
        {
            if (ShouldCreateNewLogDisplay(FilePath))
            {
                CreateNewLogDisplay<BasicLogEntry>(FilePath, DiskHelpers.GetFileName(FilePath), CreateLogDisplaySettings());
                
                if (_logService != null)
                    _logService.StartProcessing(CreateFileLogServiceSettings());   
            }
        }

        private FileLogServiceSettings CreateFileLogServiceSettings()
        {
            var settings = new FileLogServiceSettings
            {
                FilePath = FilePath,
                PollInterval = Int32.Parse(Settings.Interval),
                ShouldLogPollTicks = Settings.ShouldLogFilePollTicks
            };

            SubscribeFileLogServiceSettingsToFileMonitoringSettings(settings);
            return settings;
        }

        private void SubscribeFileLogServiceSettingsToFileMonitoringSettings(FileLogServiceSettings settings)
        {
            Settings.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ShouldLogPollTicks")
                    settings.ShouldLogPollTicks = Settings.ShouldLogFilePollTicks;

                if (e.PropertyName == "Interval")
                    settings.PollInterval = Int32.Parse(Settings.Interval);
            };
        }

        private LogDisplaySettings CreateLogDisplaySettings()
        {
            var settings = new LogDisplaySettings
            {
                ShouldLogFileChange = Settings.ShouldLogFileChange
            };

            SubscribeLogDisplaySettingsToFileMonitoringSettings(settings);
            return settings;
        }

        private void SubscribeLogDisplaySettingsToFileMonitoringSettings(LogDisplaySettings settings)
        {
            Settings.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ShouldLogFileChange")
                    settings.ShouldLogFileChange = Settings.ShouldLogFileChange;
            };
        }

        private void SetDefaultValues()
        {
#if DEBUG
            FilePath = DiskHelpers.GetExecutingPath() + "\\Testfile.txt";
#endif
        }

        public void OpenFileDialog()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = DiskHelpers.GetExecutingPath()
            };

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePath = dialog.FileName;
            }
        }
    }
}
