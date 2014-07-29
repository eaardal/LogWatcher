using System;
using System.Configuration;

namespace LogWatcher
{
    static class Config
    {
        static Config()
        {
            DefaultServerUrl = GetConfigValue<string>("HttpServer.DefaultUrl");
        }

        public static string DefaultServerUrl { get; private set; }

        private static T GetConfigValue<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
