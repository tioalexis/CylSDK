using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags.Impl
{
    /// <summary>
    /// Serializes and deserializes progress flags using JSON format.
    /// </summary>
    public class JsonProgressFlagsSerializer : IProgressFlagsSerializer
    {
        /// <summary>
        /// Serializes the provided progress flags into a JSON format string.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to serialize.</param>
        /// <returns>>An Awaitable containing the serialized JSON string.</returns>
        public Awaitable<string> SerializeAsync(Dictionary<string, bool> progressFlags)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            if (progressFlags == null)
            {
                completionSource.SetResult(string.Empty);
                return completionSource.Awaitable;
            }

            try
            {
                var serializedData = JsonConvert.SerializeObject(progressFlags);
                completionSource.SetResult(serializedData);
            }
            catch (Exception e)
            {
                completionSource.SetException(e);
            }
            
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Deserializes a JSON format string into a dictionary of progress flags.
        /// </summary>
        /// <param name="serializedData">The JSON format string to deserialize.</param>
        /// <returns>>An Awaitable containing a dictionary where each key is a progress flag and the value is true if the flag was set.</returns>
        public Awaitable<Dictionary<string, bool>> DeserializeAsync(string serializedData)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, bool>>();
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                completionSource.SetResult(new Dictionary<string, bool>());
                return completionSource.Awaitable;
            }

            try
            {
                var progressFlags = JsonConvert.DeserializeObject<Dictionary<string, bool>>(serializedData);
                completionSource.SetResult(progressFlags ?? new Dictionary<string, bool>());
            }
            catch (Exception e)
            {
                completionSource.SetException(e);
            }
            
            return completionSource.Awaitable;
        }
    }
}