using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogWatcher.Domain.Messages;
using LogWatcher.Infrastructure;

namespace LogWatcher.Domain
{
    class FileReader
    {
        private readonly Dictionary<string, int> _lineNumbersCache;

        public FileReader()
        {
            _lineNumbersCache = new Dictionary<string, int>();
        }

        public async Task<FileChangeInfo> ReadChanges(byte[] fileBytes, string identifier)
        {
            var allLines = await ReadAllLines(fileBytes);
            return await ReadNewLines(allLines, identifier);
        }

        private async Task<Dictionary<int, string>> ReadAllLines(byte[] fileBytes)
        {
            return await Task.Run(() =>
            {
                var allLines = new Dictionary<int, string>();

                using (var memoryStream = new MemoryStream(fileBytes))
                using (var bufferedStream = new BufferedStream(memoryStream))
                using (var streamReader = new StreamReader(bufferedStream))
                {
                    try
                    {
                        var lineNr = 0;
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            lineNr++;
                            allLines.Add(lineNr, line);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                return allLines;
            });
        }

        private async Task<FileChangeInfo> ReadNewLines(Dictionary<int, string> allLines, string identifier, int lineCount = 0)
        {
            return await Task.Run(() =>
            {
                var newLines = new Dictionary<int, string>();
                var currentLineCount = lineCount > 0 ? lineCount : allLines.Count();

                if (HasBeenReadPreviously(identifier))
                {
                    if (HasMoreLinesThanLastRead(identifier, currentLineCount))
                    {
                        var cachedLineCount = GetPreviousMaxLineFromCache(identifier);
                        newLines = GetNewLines(allLines, cachedLineCount, currentLineCount);

                        UpdateLineCountCache(identifier, currentLineCount);
                    }
                }
                else
                {
                    AddToLineCountCache(identifier, currentLineCount);
                    newLines = allLines;
                }

                var newLinesCount = newLines.Count;
                Message.Publish(new StatusBarMessage(identifier) { Text = "Read " + newLinesCount + " new lines" });

                return new FileChangeInfo { Identifier = identifier, ChangedLines = newLines, LineCount = newLinesCount };
            });
        }

        private Dictionary<int, string> GetNewLines(Dictionary<int, string> allLines, int cachedLineCount, int currentLineCount)
        {
            return allLines.Skip(cachedLineCount)
                           .Take(currentLineCount - cachedLineCount)
                           .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
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
