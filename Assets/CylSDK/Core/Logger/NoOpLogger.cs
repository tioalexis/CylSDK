namespace CylSDK.Core.Logger
{
    public class NoOpLogger : ILogger
    {
        public void LogInfo(object message)
        {
            // No operation
        }

        public void LogWarning(object message)
        {
            // No operation
        }

        public void LogError(object message)
        {
            // No operation
        }
    }
}