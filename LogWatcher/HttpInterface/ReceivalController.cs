using System;
using LogWatcher.Domain;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;
using Nancy;
using Nancy.ModelBinding;

namespace LogWatcher.HttpInterface
{
    public class ReceivalController : NancyModule
    {
        private readonly LogEntryValidator _validator = new LogEntryValidator();

        public ReceivalController()
        {
            Get["/"] = parameters => "This is the Log Watcher HTTP API. Send log messages by doing a POST to " + Config.DefaultServerUrl + " with a JSON object like this: \n" + LogEntry.GetAsJsonFormat();

            Post["/"] = parameters =>
            {
                try
                {
                    var logEntry = this.Bind<LogEntry>(BindingConfig.Default);

                    if (_validator.IsValid(logEntry))
                    {
                        Message.Publish(new ReceivedHttpLogEntryMessage {LogEntry = logEntry});
                        
                        return Response.AsText("Received log message with no validation errors").WithStatusCode(HttpStatusCode.OK);
                    }
                    return Response.AsText(GetBadRequestResponseText()).WithStatusCode(HttpStatusCode.BadRequest);
                }
                catch (Exception ex)
                {
                    return Response.AsText(GetBadRequestResponseText() + "\n\n The error message was: " + ex.Message).WithStatusCode(HttpStatusCode.BadRequest);
                }
            };
        }

        private string GetBadRequestResponseText()
        {
            return "Could not parse the log entry. Make sure it corresponds to the following JSON: \n" + LogEntry.GetAsJsonFormat();
        }
    }
}