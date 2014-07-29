using LogWatcher.Domain;
using Nancy;
using Nancy.ModelBinding;

namespace LogWatcher.HttpInterface
{
    public class ReceivalController : NancyModule
    {
        public ReceivalController()
        {
            Get["/"] = parameters => "This is the Log Watcher HTTP API.";

            Post["/"] = parameters =>
            {
                var logEntry = this.Bind<LogEntry>();
                return 200;
            };
        }
    }
}