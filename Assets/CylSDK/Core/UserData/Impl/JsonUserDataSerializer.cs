using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// Serializer for user data models using JSON format.
    /// Uses Newtonsoft.Json for serialization and deserialization.
    /// </summary>
    public class JsonUserDataSerializer : IUserDataSerializer
    {
        /// <summary>
        /// Serializes the given user data model to a JSON string.
        /// </summary>
        /// <param name="userData">The user data model to serialize.</param>
        /// <returns>An Awaitable string containing the serialized user data.</returns>
        public Awaitable<string> SerializeAsync(IUserDataModel userData)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            
            if (userData == null)
            {
                completionSource.TrySetException(new ArgumentNullException(nameof(userData)));
                return completionSource.Awaitable;
            }

            try
            {
                var serializedData = JsonConvert.SerializeObject(userData);
                completionSource.TrySetResult(serializedData);
            }
            catch (Exception e)
            {
                completionSource.TrySetException(e);
            }
            
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Deserializes a JSON string into a user data model of type T.
        /// </summary>
        /// <param name="serializedData">The JSON string to deserialize.</param>
        /// <typeparam name="T">The type of user data model to deserialize into. It must implement IUserDataModel.</typeparam>
        /// <returns>An Awaitable instance containing the deserialized user data model of type T.</returns>
        public Awaitable<T> DeserializeAsync<T>(string serializedData) where T : IUserDataModel
        {
            var completionSource = new AwaitableCompletionSource<T>();
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                completionSource.TrySetException(new ArgumentNullException(nameof(serializedData)));
                return completionSource.Awaitable;
            }
            
            try
            {
                var deserializedData = JsonConvert.DeserializeObject<T>(serializedData);
                completionSource.TrySetResult(deserializedData);
            }
            catch (Exception e)
            {
                completionSource.TrySetException(e);
            }
            
            return completionSource.Awaitable;
        }
    }
}