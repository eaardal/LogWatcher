using System;
using System.Configuration;

namespace LogWatcher.Infrastructure
{
    static class Config
    {
        static Config()
        {
            DefaultServerUrl = GetConfigValue<string>("HttpServer.DefaultUrl");
            DefaultPollInterval = GetConfigValue<int>("DefaultPollInterval");
        }

        public static string DefaultServerUrl { get; private set; }
        public static int DefaultPollInterval { get; private set; }

        private static T GetConfigValue<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
