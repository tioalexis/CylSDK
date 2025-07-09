namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Interface for loading and saving user data requests.
    /// </summary>
    public interface ISaveUserDataRequestsLoader
    {
        /// <summary>
        /// Saves the serialized user data requests.
        /// </summary>
        /// <param name="serializedData">The serialized data to save.</param>
        void Save(string serializedData);

        /// <summary>
        /// Loads the serialized user data requests.
        /// </summary>
        /// <returns>The serialized data that was loaded.</returns>
        string Load();

        /// <summary>
        /// Clears the saved user data requests.
        /// </summary>
        void Clear();
    }
}