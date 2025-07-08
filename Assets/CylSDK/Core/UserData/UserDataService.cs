using System;
using System.Collections.Generic;
using System.Threading;
using CylSDK.Utils;
using UnityEngine;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Service for managing user data.
    /// This service accepts a concrete implementation of IUserDataProvider and
    /// provides methods to load and save user data using that provider.
    /// </summary>
    /// <typeparam name="T">The type of user data model that implements IUserDataModel.</typeparam>
    public class UserDataService<T> where T : IUserDataModel
    {
        /// <summary>
        /// How often the save queue is checked for new save requests.
        /// </summary>
        private const float SaveQueueCheckInterval = 1.0f / 16;
        
        private struct SaveRequest
        {
            public T UserDataSnapshot;
            public DateTime RequestTime;
        }
        
        private readonly IUserDataProvider _userDataProvider;
        private readonly IUserDataSerializer _userDataSerializer;
        private readonly Queue<SaveRequest> _saveQueue = new();
        
        private readonly CancellationTokenSource _saveQueueCts;
        
        /// <summary>
        /// The user data loaded or saved by this service.
        /// </summary>
        public T UserData { get; private set; }

        /// <summary>
        /// Constructor for UserDataService.
        /// </summary>
        /// <param name="userDataProvider">The user data provider to use for loading and saving user data.</param>
        /// <param name="userDataSerializer">The user data serializer to use for serializing and deserializing user data.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the required parameters are null.</exception>
        public UserDataService(IUserDataProvider userDataProvider, IUserDataSerializer userDataSerializer)
        {
            _userDataProvider = userDataProvider ?? throw new ArgumentNullException(nameof(userDataProvider));
            _userDataSerializer = userDataSerializer ?? throw new ArgumentNullException(nameof(userDataSerializer));
            
            // Start processing the save queue in a separate thread
            _saveQueueCts = new CancellationTokenSource();
            ProcessSaveQueueAsync().FireAndForget();
        }

        ~UserDataService()
        {
            // Store the requests into disk for later processing
            if (_saveQueue.Count > 0)
            {
                Debug.LogWarning("UserDataService is being finalized with pending save requests. " +
                                 "These requests will not be processed.");
                _saveQueue.Clear();
            }
            
            // Cancel the save queue processing when the service is disposed
            _saveQueueCts.Cancel();
            _saveQueueCts.Dispose();
        }
        
        /// <summary>
        /// Creates a new user data instance using the provided user data provider.
        /// </summary>
        /// <returns>An awaitable task that returns the newly created user data.</returns>
        public async Awaitable<T> CreateUserDataAsync()
        {
            UserData = (T)await _userDataProvider.CreateUserDataAsync(_userDataSerializer);
            return UserData;
        }

        /// <summary>
        /// Asynchronously loads user data using the provided user data provider.
        /// </summary>
        /// <returns>An awaitable task that returns the loaded user data.</returns>
        public async Awaitable<T> LoadUserDataAsync()
        {
            UserData = (T)await _userDataProvider.LoadUserDataAsync(_userDataSerializer);
            return UserData;
        }
        
        /// <summary>
        /// Enqueues a save request for the current user data.
        /// </summary>
        public void SaveUserData()
        {
            var saveRequest = new SaveRequest
            {
                UserDataSnapshot = UserData,
                RequestTime = DateTime.UtcNow
            };
            
            _saveQueue.Enqueue(saveRequest);
        }
        
        private async Awaitable ProcessSaveQueueAsync() 
        {
            await Awaitable.MainThreadAsync();
            
            // Periodically check the save queue and process requests
            while (!_saveQueueCts.IsCancellationRequested)
            {
                if (_saveQueue.Count > 0)
                {
                    var saveRequest = _saveQueue.Dequeue();
                    await _userDataProvider.SaveUserDataAsync(saveRequest.UserDataSnapshot, _userDataSerializer);
                }
                
                // Wait for a short period before checking the queue again
                await Awaitable.WaitForSecondsAsync(SaveQueueCheckInterval);
            }
            
            // When cancellation is requested, ensure all remaining save requests are processed
            while (_saveQueue.Count > 0)
            {
                var saveRequest = _saveQueue.Dequeue();
                await _userDataProvider.SaveUserDataAsync(saveRequest.UserDataSnapshot, _userDataSerializer);
            }
            
            // Clean up the cancellation token source
            _saveQueueCts.Dispose();
            _saveQueue.Clear();
        }
    }
}