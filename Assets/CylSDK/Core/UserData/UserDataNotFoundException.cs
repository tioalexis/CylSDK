namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Thrown when user data is not found in the system.
    /// When this happens, it usually means that it is the first time the user is using the application,
    /// or the user data has been deleted or corrupted.
    /// </summary>
    public class UserDataNotFoundException : System.Exception
    {
        /// <summary>
        /// Constructs a new UserDataNotFoundException with a default message.
        /// </summary>
        public UserDataNotFoundException() : base("Could not find user data. " +
                                                  "This usually means that it is the first time the user is using the application, " +
                                                  "or the user data has been deleted or corrupted.") { }
        
        /// <summary>
        /// Constructs a new UserDataNotFoundException with a default message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UserDataNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Constructs a new UserDataNotFoundException with a specified message and an inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UserDataNotFoundException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}