using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWatcher.HttpInterface;

namespace LogWatcher.Domain
{
    class HttpLogService
    {
        public Action<HttpLogEntry> NewLogEntryCallback { get; set; }

        public void StartProcessing()
        {
            StartHttpServer();

        }

        public void StartHttpServer()
        {
            var http = new LogWatcherHttpServer();
            http.Connect(Config.DefaultServerUrl);
        }
    }
}
