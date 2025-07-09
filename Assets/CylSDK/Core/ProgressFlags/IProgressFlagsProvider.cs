using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags
{
    /// <summary>
    /// Provides methods to create, read, and write progress flags data.
    /// This interface is used to abstract the storage and retrieval of progress flags,
    /// which can be done through various implementations such as PlayerPrefs, files, or online databases.
    /// </summary>
    public interface IProgressFlagsProvider
    {
        /// <summary>
        /// Creates an empty data structure for progress flags.
        /// The implementation should return a dictionary with the default state of progress flags.
        /// </summary>
        /// <returns>A dictionary representing the initial state of progress flags.</returns>
        Awaitable<Dictionary<string, bool>> CreateDataAsync();
        
        /// <summary>
        /// Reads progress flags data asynchronously using the provided serializer.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>An Awaitable containing a dictionary where each key is a progress flag and the value is true if the flag was set.</returns>
        Awaitable<Dictionary<string, bool>> ReadDataAsync(IProgressFlagsSerializer serializer);
        
        /// <summary>
        /// Writes the provided progress flags data asynchronously using the provided serializer.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        /// <returns>>An Awaitable that completes when the data has been written.</returns>
        Awaitable WriteDataAsync(Dictionary<string, bool> progressFlags, IProgressFlagsSerializer serializer);
    }
}