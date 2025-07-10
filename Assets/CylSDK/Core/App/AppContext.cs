using UnityEngine;

namespace CylSDK.Core.App
{
    /// <summary>
    /// Contains the context for the application.
    /// Holds references to dependencies that may be used throughout the application,
    /// such as logging and service locators.
    ///
    /// There should be only one instance of this class in the application.
    /// </summary>
    public class AppContext
    {
        /// <summary>
        /// The service locator used to manage and retrieve services throughout the application.
        /// </summary>
        public ServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Constructs a new instance of the AppContext.
        /// </summary>
        public AppContext()
        {
            ServiceLocator = new ServiceLocator(this);
        }

        /// <summary>
        /// Notifies the application to perform any necessary cleanup or teardown operations when the context is destroyed.
        /// </summary>
        ~AppContext()
        {
            Teardown();
        }

        /// <summary>
        /// Initializes the application context and all registered services asynchronously.
        /// This method should be called once at the start of the application lifecycle.
        /// This method will not be called automatically, so it must be invoked explicitly by the application.
        /// </summary>
        public async Awaitable InitializeAsync()
        {
            foreach (var service in ServiceLocator.GetAllServices())
            {
                await service.InitializeAsync(this);
            }
        }

        /// <summary>
        /// Tells the application to perform any necessary cleanup or teardown operations.
        /// </summary>
        public void Teardown()
        {
            foreach (var service in ServiceLocator.GetAllServices())
            {
                service.Teardown();
            }
        }
    }
}