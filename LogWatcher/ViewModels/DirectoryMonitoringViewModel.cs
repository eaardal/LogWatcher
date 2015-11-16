using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogWatcher.Annotations;
using LogWatcher.Domain;
using LogWatcher.Domain.Helpers;
using LogWatcher.Domain.Settings;

namespace LogWatcher.ViewModels
{
    class DirectoryMonitoringViewModel : MonitoringViewModelBase
    {
        private readonly ILogService _logService;
        private string _directoryPath;
        private DirectoryMonitoringSettings _settings;

        public DirectoryMonitoringViewModel([NotNull] ILogService logService)
        {
            if (logService == null) throw new ArgumentNullException(nameof(logService));
            _logService = logService;

            _settings = new DirectoryMonitoringSettings();

            SetDefaultValues();
        }

        public string DirectoryPath
        {
            get { return _directoryPath; }
            set
            {
                if (value == _directoryPath) return;
                _directoryPath = value;
                NotifyPropertyChange();
            }
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

        public void StartPolling()
        {
            if (ShouldCreateNewLogDisplay(DirectoryPath))
            {
                var identifier = DirectoryPath;
                var displayTitle = DiskHelpers.GetDirectoryName(DirectoryPath);
                var settings = CreateLogDisplaySettings();

                CreateNewLogDisplay<BasicLogEntry>(identifier, displayTitle, settings);

                var logServiceSettings = CreateDirectoryLogServiceSettings();

                _logService.StartProcessing(logServiceSettings);
            }
        }

        private DirectoryLogServiceSettings CreateDirectoryLogServiceSettings()
        {
            var settings = new DirectoryLogServiceSettings
            {
                FilesChangedSinceTimestamp = Settings.FilesChangedSinceTimestamp,
                FilesChangedToday = Settings.FilesChangedToday,
                LastChangedFile = Settings.LastChangedFile,
                Timestamp = Settings.Timestamp
            };

            SubscribeDirectoryLogServiceSettingsToDirectoryMonitoringSettingChanges(settings);

            return settings;
        }

        private void SubscribeDirectoryLogServiceSettingsToDirectoryMonitoringSettingChanges(DirectoryLogServiceSettings settings)
        {
            Settings.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(settings.FilesChangedToday))
                    settings.FilesChangedToday = Settings.FilesChangedToday;

                if (e.PropertyName == nameof(settings.FilesChangedSinceTimestamp))
                    settings.FilesChangedSinceTimestamp = Settings.FilesChangedSinceTimestamp;

                if (e.PropertyName == nameof(settings.LastChangedFile))
                    settings.LastChangedFile = Settings.LastChangedFile;

                if (e.PropertyName == nameof(settings.Timestamp))
                    settings.Timestamp = Settings.Timestamp;
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
                if (e.PropertyName == nameof(settings.ShouldLogFileChange))
                    settings.ShouldLogFileChange = Settings.ShouldLogFileChange;
            };
        }

        private void SetDefaultValues()
        {
#if DEBUG
            var path = DiskHelpers.GetExecutingPath() + "\\TestDirectory\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DirectoryPath = path;
#endif
        }

        public void OpenFileDialog()
        {
            var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                DirectoryPath = dialog.SelectedPath;
            }
        }
    }
}
