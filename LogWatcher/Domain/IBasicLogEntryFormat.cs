namespace LogWatcher.Domain
{
    internal interface IBasicLogEntryFormat
    {
        BasicLogEntry Parse(string identifier, string text, int lineNr);
    }
}