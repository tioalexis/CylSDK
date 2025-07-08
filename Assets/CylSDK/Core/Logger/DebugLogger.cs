using UnityEngine;

namespace CylSDK.Core.Logger
{
    public class DebugLogger : ILogger
    {
        public void LogInfo(object message)
        {
            Debug.Log(message);
        }

        public void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(object message)
        {
            Debug.LogError(message);
        }
    }
}