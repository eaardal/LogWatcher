using System;
using System.IO;
using System.Security.Cryptography;
using System.Timers;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain
{
    class FilePoller
    {
        private readonly FileInfo _fileToWatch;
        private readonly int _pollInterval;
        private Timer _pollTimer;
        private string _lastFileHash;

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

            Message.Publish(new FilePollTickMessage{ File = _fileToWatch, Sender = this });
            
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_fileToWatch.FullName))
                {
                    var hash = BitConverter.ToString(md5.ComputeHash(stream)); ;

                    if (hash != _lastFileHash)
                    {
                        Message.Publish(new FileChangeDetectedMessage { File = _fileToWatch,  Sender = this });
                        UpdateLastFileHash(hash);
                    }
                }
            }

            _pollTimer.Enabled = true;
        }

        private void UpdateLastFileHash(string hash)
        {
            _lastFileHash = hash;
        }
    }
}
