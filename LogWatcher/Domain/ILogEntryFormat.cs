namespace LogWatcher.Domain
{
    internal interface ILogEntryFormat
    {
        LogEntry Parse(string identifier, string text);
    }
}