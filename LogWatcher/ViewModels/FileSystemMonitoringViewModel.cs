using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using LogWatcher.Annotations;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;
using Message = LogWatcher.Infrastructure.Message;

namespace LogWatcher.ViewModels
{
    class FileSystemMonitoringViewModel : MonitoringViewModelBase
    {
        private readonly ILogService _logService;
        private string _lastPollTime;
        private string _filePath;
        private string _interval;
        
        public FileSystemMonitoringViewModel([NotNull] ILogService logService)
        {
            if (logService == null) throw new ArgumentNullException("logService");
            _logService = logService;

            SetDefaultValues();

            Message.Subscribe<FilePollTickMessage>(OnFilePollTick);
        }
        
        public string Interval
        {
            get { return _interval; }
            set
            {
                if (value == _interval) return;
                _interval = value;
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

        public void StartPolling()
        {
            if (ShouldCreateNewLogDisplay(FilePath))
            {
                CreateNewLogDisplay<BasicLogEntry>(FilePath, GetFileName(FilePath));

                if (_logService != null)
                    _logService.StartProcessing(FilePath, Interval);   
            }
        }

        private string GetExecutingPath()
        {
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            return currentLocation.Substring(0, currentLocation.LastIndexOf('\\'));
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

        public void OpenFileDialog()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = GetExecutingPath()
            };

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePath = dialog.FileName;
            }
        }

        private void SetDefaultValues()
        {
            Interval = Config.DefaultPollInterval.ToString(CultureInfo.InvariantCulture);
#if DEBUG
            FilePath = GetExecutingPath() + "\\Testfile.txt";
#endif
        }
    }
}
