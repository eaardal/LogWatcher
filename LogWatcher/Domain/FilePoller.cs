using System;
using System.IO;
using System.Security.Cryptography;
using System.Timers;

namespace LogWatcher.Domain
{
    internal delegate void FilePollTick(FilePoller source, FileInfo file);
    internal delegate void FileHasChanges(FilePoller source, FileInfo file);

    class FilePoller
    {
        private readonly FileInfo _fileToWatch;
        private readonly int _pollInterval;
        private Timer _pollTimer;
        private string _lastFileHash;

        public event FilePollTick FilePollTick;
        public event FileHasChanges FileHasChanges;

        public FilePoller(FileInfo fileToWatch, int pollInterval = 1000)
        {
            if (fileToWatch == null) throw new ArgumentNullException("fileToWatch");
            _fileToWatch = fileToWatch;
            _pollInterval = pollInterval;
        }

        public void Start()
        {
            _pollTimer = new Timer(_pollInterval);
            _pollTimer.Elapsed += OnPollTimerTick;
            _pollTimer.Start();
        }

        public void Stop()
        {
            _pollTimer.Stop();
            _pollTimer = null;
        }

        private void OnPollTimerTick(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _pollTimer.Enabled = false;

            if (FilePollTick != null)
            {
                FilePollTick(this, _fileToWatch);
            }

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_fileToWatch.FullName))
                {
                    var hash = BitConverter.ToString(md5.ComputeHash(stream)); ;

                    if (hash != _lastFileHash)
                    {
                        NotifyOfFileChange();
                        UpdateLastFileHash(hash);
                    }
                }
            }

            _pollTimer.Enabled = true;
        }

        private void NotifyOfFileChange()
        {
            if (FileHasChanges != null)
            {
                FileHasChanges(this, _fileToWatch);
            }
        }

        private void UpdateLastFileHash(string hash)
        {
            _lastFileHash = hash;
        }
    }
}
