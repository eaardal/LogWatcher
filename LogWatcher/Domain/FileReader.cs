using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogWatcher.Domain
{
    class FileReader
    {
        private readonly Dictionary<FileInfo, int> _lineNumbersCache;

        public FileReader()
        {
            _lineNumbersCache = new Dictionary<FileInfo, int>();
        }

        public async Task<IEnumerable<string>> ReadChanges(FileInfo file)
        {
            return await Task.Run(() =>
            {
                var allLines = File.ReadAllLines(file.FullName);
                var currentLineCount = allLines.Count();

                if (HasBeenReadPreviously(file))
                {
                    if (HasMoreLinesThanLastRead(file, currentLineCount))
                    {
                        var cachedLineCount = GetPreviousMaxLineFromCache(file);
                        var newLines = GetNewLines(allLines, cachedLineCount, currentLineCount);

                        UpdateLineCountCache(file, currentLineCount);

                        return newLines;
                    }
                }
                else
                {
                    AddToLineCountCache(file, currentLineCount);
                    return allLines;
                }
                return new List<string>();
            });
        }

        private IEnumerable<string> GetNewLines(IEnumerable<string> allLines, int cachedLineCount, int currentLineCount)
        {
            return allLines.Skip(cachedLineCount).Take(currentLineCount - cachedLineCount);
        }

        private void UpdateLineCountCache(FileInfo file, int count)
        {
            _lineNumbersCache[file] = count;
        }

        private bool HasMoreLinesThanLastRead(FileInfo file, int currentLineCount)
        {
            return currentLineCount > GetPreviousMaxLineFromCache(file);
        }

        private int GetPreviousMaxLineFromCache(FileInfo file)
        {
            return _lineNumbersCache[file];
        }

        private bool HasBeenReadPreviously(FileInfo file)
        {
            return _lineNumbersCache.ContainsKey(file);
        }

        private void AddToLineCountCache(FileInfo file, int currentLineCount)
        {
            _lineNumbersCache.Add(file, currentLineCount);
        }
    }
}
