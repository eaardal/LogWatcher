using System;
using System.Reflection;
using LogWatcher.Annotations;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.ViewModels
{
    class FileSystemMonitoringViewModel : MonitoringViewModelBase
    {
        private readonly ILogService _logService;
        private string _lastPollTime;

        public FileSystemMonitoringViewModel([NotNull] ILogService logService)
        {
            if (logService == null) throw new ArgumentNullException("logService");
            _logService = logService;
            
            Message.Subscribe<FilePollTickMessage>(OnFilePollTick);
        }

        public string LastPollTime
        {
            get { return _lastPollTime; }
            private set
            {
                if (value == _lastPollTime) return;
                _lastPollTime = value;
                NotifyPropertyChange();
            }
        }

        public string GetTestFilePath()
        {
            return GetExecutingPath() + "\\Testfile.txt";
        }

        public string GetExecutingPath()
        {
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            return currentLocation.Substring(0, currentLocation.LastIndexOf('\\'));
        }

        public void StartPolling(string filepath)
        {
            if (ShouldCreateNewLogDisplay(filepath))
            {
                CreateNewLogDisplay<BasicLogEntry>(filepath, GetFileName(filepath));

                if (_logService != null)
                    _logService.StartProcessing(filepath);   
            }
        }

        private void OnFilePollTick(FilePollTickMessage obj)
        {
            LastPollTime = DateTime.Now.ToLongTimeString();
        }

        private string GetFileName(string filepath)
        {
            var lastIndex = filepath.LastIndexOf("\\", StringComparison.Ordinal);
            var startindex = lastIndex + 1;
            var length = filepath.Length;
            return filepath.Substring(startindex, length - startindex);
        }
    }
}
