using System;
using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserStats.Impl
{
    /// <summary>
    /// Provides user stats storage using Unity's PlayerPrefs.
    /// </summary>
    public class PlayerPrefsUserStatsProvider : IUserStatsProvider
    {
        private const string PlayerPrefsKey = "CylSDK.UserStats";
        
        /// <summary>
        /// Creates an empty user stats data dictionary.
        /// </summary>
        /// <returns>An Awaitable instance containing an empty dictionary.</returns>
        public Awaitable<Dictionary<string, double>> CreateDataAsync()
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, double>>();
            completionSource.SetResult(new Dictionary<string, double>());
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Reads user stats data from PlayerPrefs using the provided serializer.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>An Awaitable instance containing the deserialized user stats data.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the serializer is null.</exception>
        public async Awaitable<Dictionary<string, double>> ReadDataAsync(IUserStatsSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            var serializedData = PlayerPrefs.GetString(PlayerPrefsKey, string.Empty);
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                return new Dictionary<string, double>();
            }
            
            return await serializer.DeserializeAsync(serializedData);
        }

        /// <summary>
        /// Writes the provided user stats data to PlayerPrefs using the specified serializer.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        /// <exception cref="ArgumentNullException">Thrown if the data or serializer is null.</exception>
        public async Awaitable WriteDataAsync(Dictionary<string, double> data, IUserStatsSerializer serializer)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            var serializedData = await serializer.SerializeAsync(data);
            PlayerPrefs.SetString(PlayerPrefsKey, serializedData);
            PlayerPrefs.Save();
        }
    }
}