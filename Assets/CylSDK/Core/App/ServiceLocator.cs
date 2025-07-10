using System;
using System.Collections.Generic;
using UnityEngine;

namespace CylSDK.Core.App
{
    /// <summary>
    /// Represents a service that can be registered with the ServiceLocator.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Initializes the service asynchronously.
        /// </summary>
        /// <param name="context">The application context that provides dependencies.</param>
        /// <returns>An awaitable task that completes when the service is initialized.</returns>
        Awaitable InitializeAsync(AppContext context);

        /// <summary>
        /// Notifies the service to perform any necessary cleanup or teardown operations.
        /// </summary>
        void Teardown();
    }
    
    /// <summary>
    /// This class serves as a service locator for the entire application.
    /// Only one type of each service can be registered at a time.
    /// </summary>
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new();

        public ServiceLocator(AppContext context)
        {
            
        }
        
        /// <summary>
        /// Retrieves a list of all registered services.
        /// </summary>
        /// <returns>A list of all registered services.</returns>
        public List<IService> GetAllServices()
        {
            return new List<IService>(_services.Values);
        }
        
        /// <summary>
        /// Registers a new service or updates an existing one.
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterService<T>(T service) where T : IService
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service of type {type} is already registered. Existing service will be replaced.");
            }
            
            _services[type] = service;
        }
        
        /// <summary>
        /// Retrieves a service of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve.</typeparam>
        /// <returns>The service of the specified type.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the service of the specified type is not registered.</exception>
        public T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            else
            {
                throw new KeyNotFoundException($"Service of type {type.Name} not found.");
            }
        }
        
        /// <summary>
        /// Attempts to retrieve a service of the specified type.
        /// </summary>
        /// <param name="service">The service of the specified type if found; otherwise, default value.</param>
        /// <typeparam name="T">The type of the service to retrieve.</typeparam>
        /// <returns>True if the service was found; otherwise, false.</returns>
        public bool TryGetService<T>(out T service) where T : IService
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var foundService))
            {
                service = (T)foundService;
                return true;
            }
            else
            {
                service = default;
                return false;
            }
        }
        
        /// <summary>
        /// Removes a service of the specified type from the service locator.
        /// </summary>
        /// <typeparam name="T">The type of the service to unregister.</typeparam>
        public void UnregisterService<T>() where T : IService
        {
            _services.Remove(typeof(T));
        }
    }
}