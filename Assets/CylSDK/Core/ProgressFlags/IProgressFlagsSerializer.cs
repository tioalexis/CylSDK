using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags
{
    /// <summary>
    /// Serializes and deserializes progress flags.
    /// </summary>
    public interface IProgressFlagsSerializer
    {
        /// <summary>
        /// Serializes the provided progress flags into a string format.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to serialize.</param>
        /// <returns>>An Awaitable containing the serialized string.</returns>
        Awaitable<string> SerializeAsync(Dictionary<string, bool> progressFlags);
        
        /// <summary>
        /// Deserializes a string format into a dictionary of progress flags.
        /// </summary>
        /// <param name="serializedData">The string format to deserialize.</param>
        /// <returns>An Awaitable containing a dictionary where each key is a progress flag and the value is true if the flag was set.</returns>
        Awaitable<Dictionary<string, bool>> DeserializeAsync(string serializedData);
    }
}