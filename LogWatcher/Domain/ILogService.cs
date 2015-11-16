using LogWatcher.Domain.Settings;

namespace LogWatcher.Domain
{
    internal interface ILogService
    {
        void StartProcessing(ILogServiceSettings settings);
    }
}