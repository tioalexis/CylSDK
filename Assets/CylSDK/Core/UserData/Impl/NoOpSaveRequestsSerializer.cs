using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// No-op implementation of ISaveUserDataRequestsSerializer that performs no serialization or deserialization.
    /// All operations will succeed immediately and return empty results.
    /// </summary>
    public class NoOpSaveRequestsSerializer : ISaveUserDataRequestsSerializer
    {
        /// <summary>
        /// Does not initialize any serializer.
        /// </summary>
        /// <param name="userDataSerializer">The user data serializer to initialize with.</param>
        public void Initialize(IUserDataSerializer userDataSerializer)
        {
            // NoOp
        }

        /// <summary>
        /// Does not serialize any save requests.
        /// </summary>
        /// <param name="saveRequests">The collection of save requests to serialize.</param>
        /// <returns>An empty string as no serialization is performed.</returns>
        public string Serialize<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel
        {
            return string.Empty; // No serialization performed
        }

        /// <summary>
        /// Does not serialize any save requests.
        /// </summary>
        /// <param name="saveRequests">The collection of save requests to serialize.</param>
        /// <typeparam name="T">The type of user data model contained in the save requests. Must implement IUserDataModel.</typeparam>
        /// <returns>An Awaitable string that completes immediately with an empty result.</returns>
        public Awaitable<string> SerializeAsync<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<string>();
            completionSource.SetResult(string.Empty); // No serialization performed
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not deserialize any save requests.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <returns>An empty collection of SaveUserDataRequest&lt;IUserDataModel&gt; as no deserialization is performed.</returns>
        public IEnumerable<SaveUserDataRequest<T>> Deserialize<T>(string serializedData) where T : IUserDataModel
        {
            return new List<SaveUserDataRequest<T>>(); // No deserialization performed
        }

        /// <summary>
        /// Does not deserialize any save requests.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <typeparam name="T">The type of user data model to deserialize into. Must implement IUserDataModel.</typeparam>
        /// <returns>An Awaitable instance containing an empty collection of SaveUserDataRequest&lt;T&gt;.</returns>
        public Awaitable<IEnumerable<SaveUserDataRequest<T>>> DeserializeAsync<T>(string serializedData) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<IEnumerable<SaveUserDataRequest<T>>>();
            completionSource.SetResult(new List<SaveUserDataRequest<T>>()); // No deserialization performed
            return completionSource.Awaitable;
        }
    }
}