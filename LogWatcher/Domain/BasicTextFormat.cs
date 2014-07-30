namespace LogWatcher.Domain
{
    internal class BasicTextFormat : IBasicLogEntryFormat
    {
        public BasicLogEntry Parse(string identifier, string text)
        {
            return new BasicLogEntry { Text = text, SourceIdentifier = identifier };
        }
    }
}