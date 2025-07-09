using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// Serializer for save requests that uses JSON serialization.
    /// Uses Newtonsoft.Json for serialization and deserialization.
    /// </summary>
    public class JsonSaveRequestsSerializer : ISaveUserDataRequestsSerializer
    {
        private IUserDataSerializer _userDataSerializer;
        
        /// <inheritdoc/>
        public void Initialize(IUserDataSerializer userDataSerializer)
        {
            _userDataSerializer = userDataSerializer;
        }

        /// <inheritdoc/>
        public string Serialize<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel
        {
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(saveRequests);
            return serializedData;
        }

        /// <inheritdoc/>
        public Awaitable<string> SerializeAsync<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<string>();
            if (_userDataSerializer == null)
            {
                completionSource.SetException(new System.InvalidOperationException("User data serializer is not initialized."));
                return completionSource.Awaitable;
            }
            
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(saveRequests);
            completionSource.SetResult(serializedData);
            return completionSource.Awaitable;
        }

        /// <inheritdoc/>
        public IEnumerable<SaveUserDataRequest<T>> Deserialize<T>(string serializedData) where T : IUserDataModel
        {
            var deserializedData = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SaveUserDataRequest<T>>>(serializedData);
            return deserializedData;
        }

        /// <inheritdoc/>
        public Awaitable<IEnumerable<SaveUserDataRequest<T>>> DeserializeAsync<T>(string serializedData) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<IEnumerable<SaveUserDataRequest<T>>>();
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                completionSource.SetException(new System.InvalidOperationException("User data serializer is not initialized."));
                return completionSource.Awaitable;
            }
            
            try
            {
                var deserializedData = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SaveUserDataRequest<T>>>(serializedData);
                completionSource.SetResult(deserializedData);
            }
            catch (System.Exception e)
            {
                completionSource.SetException(e);
            }
            
            return completionSource.Awaitable;
        }
    }
}