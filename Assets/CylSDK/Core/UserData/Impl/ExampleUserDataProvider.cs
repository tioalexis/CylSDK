using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// Example implementation of IUserDataProvider.
    /// This class does not implement any functionality but serves as a template for creating user data providers.
    /// </summary>
    public class ExampleUserDataProvider : IUserDataProvider
    {
        public async Awaitable<IUserDataModel> CreateUserDataAsync(IUserDataSerializer serializer)
        {
            // This method should create a new user data model asynchronously.
            // In its simplest form, it can return a default instance of IUserDataModel.
            // However, in a real implementation, you would typically create a new instance of a concrete
            // class that implements IUserDataModel. This data model could come from an API call, a database, or any other source.
            // We can use the given serializer to deserialize the data we receive if needed.
            await Awaitable.MainThreadAsync();
            throw new System.NotImplementedException();
        }

        public async Awaitable<IUserDataModel> ReadUserDataAsync(IUserDataSerializer serializer)
        {
            // This method should load user data asynchronously using the provided serializer.
            // In a real implementation, you would typically retrieve the user data from a storage mechanism
            // such as PlayerPrefs, a file, or an online database, and then deserialize it using the serializer.
            await Awaitable.MainThreadAsync();
            throw new System.NotImplementedException();
        }

        public async Awaitable WriteUserDataAsync(IUserDataModel userData, IUserDataSerializer serializer)
        {
            // This method should save the user data model asynchronously using the provided serializer.
            // In a real implementation, you would typically serialize the user data model to a string or binary format
            // and then store it in a storage mechanism such as PlayerPrefs, a file, or an online database.
            await Awaitable.MainThreadAsync();
            throw new System.NotImplementedException();
        }
    }
}