using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserStats
{
    /// <summary>
    /// Serializer interface for user stats data.
    /// </summary>
    public interface IUserStatsSerializer
    {
        /// <summary>
        /// Serializes the provided user stats data into a string format.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to serialize.</param>
        /// <returns>An Awaitable string containing the serialized data.</returns>
        Awaitable<string> SerializeAsync(Dictionary<string, double> data);
        
        /// <summary>
        /// Deserializes the provided string into a dictionary of user stats data.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <returns>An Awaitable instance containing the deserialized dictionary of user stats data.</returns>
        Awaitable<Dictionary<string, double>> DeserializeAsync(string serializedData);
    }
}