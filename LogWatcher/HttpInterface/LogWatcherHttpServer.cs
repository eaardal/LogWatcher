using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting.Self;

namespace LogWatcher.HttpInterface
{
    class LogWatcherHttpServer
    {
        private NancyHost _host;

        public void Connect(string url)
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var hostConfig = new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } };
            _host = new NancyHost(new Uri(url, UriKind.Absolute), bootstrapper, hostConfig);
            _host.Start();
        }

        public void Disconnect()
        {
            _host.Stop();
            _host.Dispose();
        }
    }
}
