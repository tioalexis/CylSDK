using System;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Represents a request to save user data.
    /// When a request is made, it contains a snapshot of the user data model and the time of the request.
    /// The request is then processed one at a time by the user data provider. When the application is closed,
    /// the requests are saved to a persistent storage and loaded when the application is started again.
    /// </summary>
    /// <typeparam name="T">The type of user data model that implements IUserDataModel.</typeparam>
    [Serializable]
    public struct SaveUserDataRequest<T> where T : IUserDataModel
    {
        /// <summary>
        /// The user data snapshot of the user data model at the time of the request.
        /// </summary>
        public T userDataSnapshot;
        
        /// <summary>
        /// The time when the request was made.
        /// </summary>
        public DateTime requestTime;
    }
}