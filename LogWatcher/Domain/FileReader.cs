using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogWatcher.Domain
{
    class FileReader
    {
        private readonly Dictionary<string, int> _lineNumbersCache;

        public FileReader()
        {
            _lineNumbersCache = new Dictionary<string, int>();
        }

        public async Task<IEnumerable<string>> ReadChanges(FileStream stream, string identifier)
        {
            return await Task.Run(async () =>
            {
                var allLines = new List<string>();

                using (var sr = new StreamReader(stream))
                {
                    allLines.Add(await sr.ReadLineAsync());
                }

                return await ReadChanges(allLines, identifier);
            });
        }

        public async Task<IEnumerable<string>> ReadChanges(byte[] fileBytes, string identifier)
        {
            return await Task.Run(async () =>
            {
                var allLines = new List<string>();
                var lineCount = 0;

                using (var memoryStream = new MemoryStream(fileBytes))
                using (var bufferedStream = new BufferedStream(memoryStream))
                using (var streamReader = new StreamReader(bufferedStream))
                {
                    try
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            allLines.Add(line);
                            lineCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

                return await ReadChanges(allLines, identifier, lineCount);
            });
        }

        private async Task<IEnumerable<string>> ReadChanges(IReadOnlyCollection<string> allLines, string identifier, int lineCount = 0)
        {
            return await Task.Run(() =>
            {
                var currentLineCount = lineCount > 0 ? lineCount : allLines.Count();

                if (HasBeenReadPreviously(identifier))
                {
                    if (HasMoreLinesThanLastRead(identifier, currentLineCount))
                    {
                        var cachedLineCount = GetPreviousMaxLineFromCache(identifier);
                        var newLines = GetNewLines(allLines, cachedLineCount, currentLineCount);

                        UpdateLineCountCache(identifier, currentLineCount);

                        return newLines;
                    }
                }
                else
                {
                    AddToLineCountCache(identifier, currentLineCount);
                    return allLines;
                }
                return new List<string>();
            });
        }

        public async Task<IEnumerable<string>> ReadChanges(FileInfo file)
        {
            return await Task.Run(async () =>
            {
                var allLines = File.ReadAllLines(file.FullName);
                return await ReadChanges(allLines, file.FullName);
            });
        }

        private IEnumerable<string> GetNewLines(IEnumerable<string> allLines, int cachedLineCount, int currentLineCount)
        {
            return allLines.Skip(cachedLineCount).Take(currentLineCount - cachedLineCount);
        }

        private void UpdateLineCountCache(string file, int count)
        {
            _lineNumbersCache[file] = count;
        }

        private bool HasMoreLinesThanLastRead(string file, int currentLineCount)
        {
            return currentLineCount > GetPreviousMaxLineFromCache(file);
        }

        private int GetPreviousMaxLineFromCache(string file)
        {
            return _lineNumbersCache[file];
        }

        private bool HasBeenReadPreviously(string file)
        {
            return _lineNumbersCache.ContainsKey(file);
        }

        private void AddToLineCountCache(string file, int currentLineCount)
        {
            _lineNumbersCache.Add(file, currentLineCount);
        }
    }
}
