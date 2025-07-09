using UnityEngine;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// This interface defines methods for serializing and deserializing user data models.
    /// The user data may be serialized to a string format for storage or transmission.
    /// </summary>
    public interface IUserDataSerializer
    {
        /// <summary>
        /// Serializes the user data model to a string format.
        /// </summary>
        /// <param name="userData">The user data model to serialize.</param>
        /// <returns>A string representation of the user data model.</returns>
        Awaitable<string> SerializeAsync(IUserDataModel userData);
        
        /// <summary>
        /// Deserializes a string representation of user data back into a user data model.
        /// </summary>
        /// <param name="serializedData">The serialized user data string.</param>
        /// <typeparam name="T">The type of user data model to deserialize into. It must implement IUserDataModel.</typeparam>
        /// <returns>An instance of the user data model type T populated with the deserialized data.</returns>
        Awaitable<T> DeserializeAsync<T>(string serializedData) where T : IUserDataModel;
    }
}