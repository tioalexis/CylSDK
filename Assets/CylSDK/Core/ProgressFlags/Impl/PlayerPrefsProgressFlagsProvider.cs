using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags.Impl
{
    /// <summary>
    /// Stores and retrieves progress flags using Unity's PlayerPrefs.
    /// </summary>
    public class PlayerPrefsProgressFlagsProvider : IProgressFlagsProvider
    {
        private const string PlayerPrefsKey = "CylSDK.ProgressFlags";
        
        /// <summary>
        /// Creates an empty data structure for progress flags.
        /// </summary>
        /// <returns>An empty dictionary representing progress flags.</returns>
        public Awaitable<Dictionary<string, bool>> CreateDataAsync()
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, bool>>();
            completionSource.SetResult(new Dictionary<string, bool>());
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Reads progress flags from PlayerPrefs.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>A dictionary containing the progress flags read from PlayerPrefs.</returns>
        public async Awaitable<Dictionary<string, bool>> ReadDataAsync(IProgressFlagsSerializer serializer)
        {
            var serializedData = PlayerPrefs.GetString(PlayerPrefsKey, string.Empty);
            if (string.IsNullOrWhiteSpace(serializedData))
                return new Dictionary<string, bool>();
            return await serializer.DeserializeAsync(serializedData);
        }

        /// <summary>
        /// Writes the provided progress flags to PlayerPrefs.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        public async Awaitable WriteDataAsync(Dictionary<string, bool> progressFlags, IProgressFlagsSerializer serializer)
        {
            if (progressFlags == null)
                return;

            var serializedData = await serializer.SerializeAsync(progressFlags);
            PlayerPrefs.SetString(PlayerPrefsKey, serializedData);
            PlayerPrefs.Save();
        }
    }
}