using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserStats
{
    /// <summary>
    /// Interface for user stats providers that manage user statistics data.
    /// Data can be provided through various means such as local storage, remote servers, etc.
    /// </summary>
    public interface IUserStatsProvider
    {
        /// <summary>
        /// Creates a new data structure for user stats.
        /// </summary>
        /// <returns>The Awaitable instance containing an empty dictionary.</returns>
        Awaitable<Dictionary<string, double>> CreateDataAsync();
        
        /// <summary>
        /// Reads user stats data using the provided serializer.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>An Awaitable instance containing the deserialized user stats data.</returns>
        Awaitable<Dictionary<string, double>> ReadDataAsync(IUserStatsSerializer serializer);
        
        /// <summary>
        /// Writes the provided user stats data using the specified serializer.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        /// <returns>An Awaitable instance that completes when the data is written.</returns>
        Awaitable WriteDataAsync(Dictionary<string, double> data, IUserStatsSerializer serializer);
    }
}