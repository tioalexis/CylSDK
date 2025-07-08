using System.Collections.Generic;
using UnityEngine;
using ILogger = CylSDK.Core.Logger.ILogger;

namespace CylSDK.Core.App
{
    public interface IService
    {
        Awaitable InitializeAsync(AppContext context);

        void Teardown();
    }
    
    /// <summary>
    /// This class serves as a service locator for the entire application.
    /// Only one type of each service can be registered at a time.
    /// </summary>
    public class ServiceLocator
    {
        private readonly ILogger _logger;
        private readonly Dictionary<System.Type, IService> _services = new();

        public ServiceLocator(AppContext context)
        {
            _logger = context.Logger;
        }
        
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
                _logger.LogWarning($"Service {type.Name} already registered. Existing service will be replaced.");
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