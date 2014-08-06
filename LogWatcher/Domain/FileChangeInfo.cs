using System.Collections.Generic;

namespace LogWatcher.Domain
{
    class FileChangeInfo
    {
        public Dictionary<int, string> ChangedLines { get; set; }
        public int LineCount { get; set; }
        public string Identifier { get; set; }
    }
}
