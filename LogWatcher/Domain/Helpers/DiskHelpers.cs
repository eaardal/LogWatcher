using System;
using System.IO;
using System.Reflection;

namespace LogWatcher.Domain.Helpers
{
    static class DiskHelpers
    {
        public static string GetExecutingPath()
        {
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            return currentLocation.Substring(0, currentLocation.LastIndexOf('\\'));
        }

        public static string GetFileName(string filepath)
        {
            var lastIndex = filepath.LastIndexOf("\\", StringComparison.Ordinal);
            var startindex = lastIndex + 1;
            var length = filepath.Length;
            return filepath.Substring(startindex, length - startindex);
        }

        public static string GetDirectoryName(string path)
        {
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            var directoryPath = Path.GetDirectoryName(path);
            var lastPathComponent = Path.GetFileName(directoryPath);

            return lastPathComponent;
        }
    }
}
