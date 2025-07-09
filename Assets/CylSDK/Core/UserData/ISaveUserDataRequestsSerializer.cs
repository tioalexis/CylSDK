using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Interface for serializing and deserializing save requests for user data.
    /// Whenever the game needs to save user data, it will create a SaveUserDataRequest&lt;T&gt;
    /// and adds it into a queue. When the game is shut down while there are still requests in the queue,
    /// this serializer will be used to serialize the requests into a string format.
    /// </summary>
    public interface ISaveUserDataRequestsSerializer
    {
        /// <summary>
        /// Initializes the serializer with the provided user data serializer.
        /// </summary>
        /// <param name="userDataSerializer">The user data serializer to use for serializing and deserializing user data models.</param>
        void Initialize(IUserDataSerializer userDataSerializer);
        
        /// <summary>
        /// Non-async version of Serialize method that serializes a collection of save requests into a string format.
        /// </summary>
        /// <param name="saveRequests">The collection of save requests to serialize.</param>
        /// <returns>>A string that contains the serialized save requests.</returns>
        string Serialize<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel;

        /// <summary>
        /// Serializes a collection of save requests into a string format.
        /// </summary>
        /// <param name="saveRequests">The collection of save requests to serialize.</param>
        /// <returns>An Awaitable string that contains the serialized save requests.</returns>
        Awaitable<string> SerializeAsync<T>(IEnumerable<SaveUserDataRequest<T>> saveRequests) where T : IUserDataModel;
        
        /// <summary>
        /// Non-async version of Deserialize method that deserializes a string representation of save requests into a collection of SaveUserDataRequest&lt;T&gt;.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <returns>A collection of SaveUserDataRequest&lt;T&gt; populated with the deserialized data.</returns>
        IEnumerable<SaveUserDataRequest<T>> Deserialize<T>(string serializedData) where T : IUserDataModel;

        /// <summary>
        /// Deserializes a string representation of save requests into a collection of SaveUserDataRequest&lt;T&gt;.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <returns>An Awaitable instance containing a collection of SaveUserDataRequest&lt;T&gt; populated with the deserialized data.</returns>
        Awaitable<IEnumerable<SaveUserDataRequest<T>>> DeserializeAsync<T>(string serializedData) where T : IUserDataModel;
    }
}