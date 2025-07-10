using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.UserStats.Impl
{
    /// <summary>
    /// No-op implementation of IUserStatsProvider that does nothing.
    /// </summary>
    public class NoOpUserStatsProvider : IUserStatsProvider
    {
        /// <summary>
        /// Does not create any data and returns an empty dictionary immediately.
        /// </summary>
        /// <returns>An Awaitable instance containing an empty dictionary.</returns>
        public Awaitable<Dictionary<string, double>> CreateDataAsync()
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, double>>();
            completionSource.SetResult(new Dictionary<string, double>());
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not read any data and returns an empty dictionary immediately.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>An Awaitable instance containing an empty dictionary.</returns>
        public Awaitable<Dictionary<string, double>> ReadDataAsync(IUserStatsSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, double>>();
            completionSource.SetResult(new Dictionary<string, double>());
            return completionSource.Awaitable;
        }
        
        /// <summary>
        /// Does not write any data and completes immediately without doing anything.
        /// </summary>
        /// <param name="data">The dictionary of user stats data to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        /// <returns>An Awaitable instance that completes immediately.</returns>
        public Awaitable WriteDataAsync(Dictionary<string, double> data, IUserStatsSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult();
            return completionSource.Awaitable;
        }
    }
}