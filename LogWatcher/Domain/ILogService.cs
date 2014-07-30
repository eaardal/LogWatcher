namespace LogWatcher.Domain
{
    internal interface ILogService
    {
        void StartProcessing(params string[] parameters);
    }
}