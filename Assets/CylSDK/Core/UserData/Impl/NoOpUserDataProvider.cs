using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// No-op implementation of IUserDataProvider that does not perform any operations.
    /// All operations will succeed immediately and return null or empty results.
    /// </summary>
    public class NoOpUserDataProvider : IUserDataProvider
    {
        /// <summary>
        /// Does not create any user data.
        /// </summary>
        /// <param name="serializer">The serializer to use for serializing the user data model.</param>
        /// <returns>Always returns an Awaitable that completes with null.</returns>
        public Awaitable<IUserDataModel> CreateUserDataAsync(IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<IUserDataModel>();
            completionSource.SetResult(null); // No user data created
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not load any user data.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing user data.</param>
        /// <returns>Always returns an Awaitable that completes with null.</returns>
        public Awaitable<IUserDataModel> ReadUserDataAsync(IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<IUserDataModel>();
            completionSource.SetResult(null); // No user data loaded
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Does not save any user data.
        /// </summary>
        /// <param name="userData">The user data model to save.</param>
        /// <param name="serializer">The serializer to use for serializing the user data model.</param>
        /// <returns>Always returns an Awaitable that completes immediately.</returns>
        public Awaitable WriteUserDataAsync(IUserDataModel userData, IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult(); // No user data saved
            return completionSource.Awaitable;
        }
    }
}