using UnityEngine;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// This interface defines the contract for user data providers.
    /// A user data provider is responsible for reading and writing user data.
    /// It may use various storage mechanisms such as PlayerPrefs, files, or online databases.
    /// </summary>
    public interface IUserDataProvider
    {
        /// <summary>
        /// Creates a new user data model asynchronously.
        /// </summary>
        /// <param name="serializer">The serializer to use for serializing the user data model.</param>
        /// <returns>An awaitable task that returns a new instance of IUserDataModel.</returns>
        Awaitable<IUserDataModel> CreateUserDataAsync(IUserDataSerializer serializer);
        
        /// <summary>
        /// Reads user data asynchronously using the provided serializer.
        /// </summary>
        /// <param name="serializer">The serializer to use for deserializing user data.</param>
        /// <returns>An awaitable task that returns the loaded user data model.</returns>
        Awaitable<IUserDataModel> ReadUserDataAsync(IUserDataSerializer serializer);

        /// <summary>
        /// Writes the user data asynchronously using the provided serializer.
        /// </summary>
        /// <param name="userData">The user data model to save.</param>
        /// <param name="serializer">The serializer to use for serializing the user data model.</param>
        /// <returns>An awaitable task that completes when the user data has been saved.</returns>
        Awaitable WriteUserDataAsync(IUserDataModel userData, IUserDataSerializer serializer);
    }
}