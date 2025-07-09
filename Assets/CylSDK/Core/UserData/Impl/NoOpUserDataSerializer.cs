using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    public class NoOpUserDataSerializer : IUserDataSerializer
    {
        public Awaitable<string> SerializeAsync(IUserDataModel userData)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            completionSource.SetResult(string.Empty); // No serialization performed
            return completionSource.Awaitable;
        }

        public Awaitable<T> DeserializeAsync<T>(string serializedData) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<T>();
            completionSource.SetResult(default); // No deserialization performed
            return completionSource.Awaitable;
        }
    }
}