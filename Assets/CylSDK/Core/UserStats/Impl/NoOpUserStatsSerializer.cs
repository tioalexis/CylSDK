using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserStats.Impl
{
    /// <summary>
    /// No-op implementation of IUserStatsSerializer that does not perform any serialization or deserialization.
    /// </summary>
    public class NoOpUserStatsSerializer : IUserStatsSerializer
    {
        /// <summary>
        /// Does not serialize any data and returns an empty string immediately.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to serialize.</param>
        /// <returns>An Awaitable string that completes immediately with an empty result.</returns>
        public Awaitable<string> SerializeAsync(Dictionary<string, double> data)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            completionSource.SetResult(string.Empty); // No-op, returns empty string
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not deserialize any data and returns an empty dictionary immediately.
        /// </summary>
        /// <param name="serializedData">The serialized data string to deserialize.</param>
        /// <returns>An Awaitable instance containing an empty dictionary.</returns>
        public Awaitable<Dictionary<string, double>> DeserializeAsync(string serializedData)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, double>>();
            completionSource.SetResult(new Dictionary<string, double>()); // No-op, returns empty dictionary
            return completionSource.Awaitable;
        }
    }
}