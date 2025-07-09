using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags.Impl
{
    /// <summary>
    /// Serializes and deserializes progress flags using CSV format.
    /// Only keys that are true will be included in the serialized output.
    /// </summary>
    public class CsvProgressFlagsSerializer : IProgressFlagsSerializer
    {
        /// <summary>
        /// Serializes the provided progress flags into a CSV format string.
        /// Only the keys that are true will be included in the output.
        /// </summary>
        /// <param name="progressFlags"></param>
        /// <returns></returns>
        public Awaitable<string> SerializeAsync(Dictionary<string, bool> progressFlags)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            if (progressFlags == null || progressFlags.Count == 0)
            {
                completionSource.SetResult(string.Empty);
                return completionSource.Awaitable;
            }
            
            var stringBuilder = new System.Text.StringBuilder();
            foreach (var kvp in progressFlags)
                if (kvp.Value)
                    stringBuilder.AppendJoin(',', kvp.Key);
            
            var serializedData = stringBuilder.ToString();
            completionSource.SetResult(string.IsNullOrEmpty(serializedData) ? string.Empty : serializedData);
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Deserializes a CSV format string into a dictionary of progress flags.
        /// The resulting dictionary values will always be true for the keys present in the string.
        /// </summary>
        /// <param name="serializedData">The CSV format string to deserialize.</param>
        /// <returns>A dictionary where each key is a progress flag and the value is always true.</returns>
        public Awaitable<Dictionary<string, bool>> DeserializeAsync(string serializedData)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, bool>>();
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                completionSource.SetResult(new Dictionary<string, bool>());
                return completionSource.Awaitable;
            }

            var progressFlags = new Dictionary<string, bool>();
            var keys = serializedData.Split(',');
            foreach (var key in keys)
            {
                if (!string.IsNullOrWhiteSpace(key))
                    progressFlags[key.Trim()] = true;
            }

            completionSource.SetResult(progressFlags);
            return completionSource.Awaitable;
        }
    }
}