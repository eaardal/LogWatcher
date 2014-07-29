using System;
using System.IO;
using System.Runtime.InteropServices;
using LogWatcher.Domain;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LogWatcher.HttpInterface
{
    public class ReceivalController : NancyModule
    {
        public ReceivalController()
        {
            Get["/"] = parameters => "This is the Log Watcher HTTP API.";

            Post["/"] = parameters =>
            {
                try
                {
                    var logEntry = this.Bind<HttpLogEntry>(BindingConfig.Default);
                    Message.Publish(new ReceivedHttpLogEntryMessage { HttpLogEntry = logEntry });
                    return 200;
                }
                catch (Exception ex)
                {
                    var response =   "Could not parse the log entry. Make sure it corresponds to the following JSON: \n" +
                           JsonConvert.SerializeObject(
                               new HttpLogEntry("The name of the application which sent the log entry", DateTime.Now,
                                   "The severity of the message.", "The source of the log entry (class/file name)",
                                   "The content of the log message"), Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()}) + "\n\n The error message was: " + ex.Message;

                    return Response.AsText(response).WithStatusCode(HttpStatusCode.BadRequest);
                }
            };
        }
    }
}