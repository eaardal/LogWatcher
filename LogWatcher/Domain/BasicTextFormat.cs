namespace LogWatcher.Domain
{
    internal class BasicTextFormat : IBasicLogEntryFormat
    {
        public BasicLogEntry Parse(string identifier, string text, int lineNr)
        {
            return new BasicLogEntry { Text = text, SourceIdentifier = identifier, LineNr = lineNr};
        }
    }
}