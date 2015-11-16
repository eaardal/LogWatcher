namespace LogWatcher.Domain.Settings
{
    interface ILogServiceSettings
    {
        bool ShouldLogPollTicks { get; set; }
        int PollInterval { get; set; }
    }
}