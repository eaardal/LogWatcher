using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Timers;
using LogWatcher.Domain.Messages;
using LogWatcher.Domain.Messages.ErrorMessages;
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

        public bool ShouldLogPollTicks { get; set; }

        public void Start()
        {
            _pollTimer = new Timer(_pollInterval);
            _pollTimer.Elapsed += async (s, e) => await OnPollTimerTick(s, e);
            _pollTimer.Start();
        }

        public void Stop()
        {
            _pollTimer.Stop();
            _pollTimer = null;
        }

        private async Task OnPollTimerTick(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _pollTimer.Enabled = false;

            if (File.Exists(_fileToWatch.FullName))
            {
                if (ShouldLogPollTicks)
                    Message.Publish(new StatusBarMessage(_fileToWatch.FullName) { Text = "Polling " + _fileToWatch.FullName });

                try
                {
                    await Task.Run(() =>
                    {
                        using (var md5 = MD5.Create())
                        {
                            var fileBytes = File.ReadAllBytes(_fileToWatch.FullName);
                            var hash = BitConverter.ToString(md5.ComputeHash(fileBytes));
                            
                            if (hash != _lastFileHash)
                            {
                                Message.Publish(new FileChangeDetectedMessage { FileBytes = fileBytes, File = _fileToWatch, Sender = this });
                                UpdateLastFileHash(hash);
                            }
                        }

                        _pollTimer.Enabled = true;
                    });
                }
                catch (FileNotFoundException)
                {
                    Message.Publish(new FileNotFoundMessage { File = _fileToWatch });
                }
                catch (IOException)
                {
                    Message.Publish(new CouldNotOpenFileMessage { File = _fileToWatch });
                }
                catch (Exception ex)
                {
                    Message.Publish(new GenericExceptionMessage { Exception = ex });
                }
            }
            else
            {
                Message.Publish(new FileNotFoundMessage { File = _fileToWatch });
            }
        }

        private void UpdateLastFileHash(string hash)
        {
            _lastFileHash = hash;
        }
    }
}
