using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags.Impl
{
    /// <summary>
    /// Serializer that does nothing. It is used when no serialization is needed.
    /// </summary>
    public class NoOpProgressFlagsSerializer : IProgressFlagsSerializer
    {
        /// <summary>
        /// Does not serialize anything and returns an empty string.
        /// </summary>
        /// <param name="progressFlags">The dictionary of progress flags to serialize.</param>
        /// <returns>An empty Awaitable string.</returns>
        public Awaitable<string> SerializeAsync(Dictionary<string, bool> progressFlags)
        {
            var completionSource = new AwaitableCompletionSource<string>();
            completionSource.SetResult(string.Empty);
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not deserialize anything and returns an empty dictionary.
        /// </summary>
        /// <param name="serializedData">The serialized data to deserialize.</param>
        /// <returns>An empty Awaitable dictionary.</returns>
        public Awaitable<Dictionary<string, bool>> DeserializeAsync(string serializedData)
        {
            var completionSource = new AwaitableCompletionSource<Dictionary<string, bool>>();
            completionSource.SetResult(new Dictionary<string, bool>());
            return completionSource.Awaitable;
        }
    }
}