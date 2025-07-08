namespace CylSDK.Core.Logger
{
    public interface ILogger
    {
        void LogInfo(object message);
        void LogWarning(object message);
        void LogError(object message);
    }
}