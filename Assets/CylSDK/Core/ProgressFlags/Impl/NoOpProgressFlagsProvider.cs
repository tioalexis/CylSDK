using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags.Impl
{
    /// <summary>
    /// ProgressFlags provider that does nothing. It is used when no progress flags are needed.
    /// </summary>
    public class NoOpProgressFlagsProvider : IProgressFlagsProvider
    {
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
        /// Does not read any data and returns an empty dictionary.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing the data.</param>
        /// <returns>An empty Awaitable dictionary.</returns>
        public Awaitable<Dictionary<string, bool>> ReadDataAsync(IProgressFlagsSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, bool>>();
            completionSource.SetResult(new Dictionary<string, bool>());
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not write any data and completes immediately.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to write.</param>
        /// <param name="serializer">The serializer to use for serializing the data.</param>
        /// <returns>An Awaitable that completes immediately.</returns>
        public Awaitable WriteDataAsync(Dictionary<string, bool> progressFlags, IProgressFlagsSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult();
            return completionSource.Awaitable;
        }
    }
}