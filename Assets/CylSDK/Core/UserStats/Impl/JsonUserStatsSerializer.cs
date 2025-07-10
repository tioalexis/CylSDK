using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CylSDK.Core.UserStats.Impl
{
    /// <summary>
    /// User stats serializer that uses JSON for serialization and deserialization.
    /// </summary>
    public class JsonUserStatsSerializer : IUserStatsSerializer
    {
        /// <summary>
        /// Serializes the provided user stats data into a JSON string.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to serialize.</param>
        /// <returns>An Awaitable string containing the serialized JSON data.</returns>
        public Awaitable<string> SerializeAsync(Dictionary<string, double> data)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            if (data == null)
            {
                completionSource.SetException(new System.ArgumentNullException(nameof(data), "Data cannot be null"));
                return completionSource.Awaitable;
            }
            
            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                completionSource.SetResult(serializedData);
            }
            catch (System.Exception ex)
            {
                completionSource.SetException(ex);
            }
            
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Deserializes the provided JSON string into a dictionary of user stats data.
        /// </summary>
        /// <param name="serializedData">The JSON string to deserialize.</param>
        /// <returns>An Awaitable instance containing the deserialized dictionary of user stats data.</returns>
        public Awaitable<Dictionary<string, double>> DeserializeAsync(string serializedData)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, double>>();
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                completionSource.SetResult(new Dictionary<string, double>());
                return completionSource.Awaitable;
            }
            
            try
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, double>>(serializedData);
                completionSource.SetResult(data ?? new Dictionary<string, double>());
            }
            catch (System.Exception ex)
            {
                completionSource.SetException(ex);
            }
            
            return completionSource.Awaitable;
        }
    }
}