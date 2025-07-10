using System;
using System.Collections.Generic;
using System.Threading;
using CylSDK.Core.App;
using CylSDK.Core.UserData.Impl;
using CylSDK.Utils;
using UnityEngine;
using AppContext = CylSDK.Core.App.AppContext;

namespace CylSDK.Core.UserData
{
    /// <summary>
    /// Service for managing user data.
    /// This service accepts a concrete implementation of IUserDataProvider and
    /// provides methods to load and save user data using that provider.
    /// </summary>
    /// <typeparam name="T">The type of user data model that implements IUserDataModel.</typeparam>
    public class UserDataService<T> : IService where T : IUserDataModel
    {
        /// <summary>
        /// How often the save queue is checked for new save requests.
        /// </summary>
        private const float SaveQueueCheckInterval = 1.0f / 16;

        private readonly IUserDataProvider _userDataProvider;
        private readonly IUserDataSerializer _userDataSerializer;
        private readonly CancellationTokenSource _saveQueueCts;
        private readonly List<SaveUserDataRequest<T>> _saveQueue = new();
        private readonly ISaveUserDataRequestsSerializer _saveRequestsSerializer = new JsonSaveRequestsSerializer();
        private readonly ISaveUserDataRequestsLoader _saveRequestsLoader = new PlayerPrefsSaveUserDataRequestLoader();
        
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
            _saveQueueCts = new CancellationTokenSource();
        }

        /// <summary>
        /// Initializes the user data service.
        /// </summary>
        /// <param name="context">The application context that provides dependencies.</param>
        /// <returns>An awaitable task that completes when the service is initialized.</returns>
        public Awaitable InitializeAsync(AppContext context)
        {
            LoadRequestsFromDisk();

            ProcessSaveQueueAsync(_saveQueueCts.Token)
                .FireAndForget();

            return Awaitable.EndOfFrameAsync();
        }

        /// <summary>
        /// Notifies the service to perform any necessary cleanup or teardown operations.
        /// </summary>
        public void Teardown()
        {
            // Store the requests into disk for later processing
            SaveRequestsToDisk();

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
            UserData = (T)await _userDataProvider.ReadUserDataAsync(_userDataSerializer);
            return UserData;
        }

        /// <summary>
        /// Enqueues a save request for the current user data.
        /// </summary>
        public void SaveUserData()
        {
            var saveRequest = new SaveUserDataRequest<T>
            {
                userDataSnapshot = UserData,
                requestTime = DateTime.UtcNow
            };

            _saveQueue.Add(saveRequest);
        }

        private async Awaitable ProcessSaveQueueAsync(CancellationToken cancellationToken = default)
        {
            await Awaitable.MainThreadAsync();

            // Periodically check the save queue and process requests
            while (!_saveQueueCts.IsCancellationRequested)
            {
                if (_saveQueue.Count > 0)
                {
                    var saveRequest = _saveQueue[0];
                    _saveQueue.RemoveAt(0);
                    await _userDataProvider.WriteUserDataAsync(saveRequest.userDataSnapshot, _userDataSerializer);
                }

                // Wait for a short period before checking the queue again
                await Awaitable.WaitForSecondsAsync(SaveQueueCheckInterval, cancellationToken);
            }
        }

        private void SaveRequestsToDisk()
        {
            if (_saveQueue.Count <= 0) return;
            
            var serializedData = _saveRequestsSerializer.Serialize(_saveQueue);
            _saveRequestsLoader.Save(serializedData);
            _saveQueue.Clear();
        }

        private void LoadRequestsFromDisk()
        {
            var serializedData = _saveRequestsLoader.Load();
            if (string.IsNullOrWhiteSpace(serializedData)) return;

            var requests = _saveRequestsSerializer.Deserialize<T>(serializedData);
            foreach (var request in requests)
            {
                if (request.userDataSnapshot is { } userData)
                {
                    _saveQueue.Add(new SaveUserDataRequest<T>
                    {
                        userDataSnapshot = userData,
                        requestTime = request.requestTime
                    });
                }
            }
            
            // Sort queue by request time to ensure the oldest requests are processed first
            _saveQueue.Sort((a, b) => a.requestTime.CompareTo(b.requestTime));
            _saveRequestsLoader.Clear();
        }
        
#if UNITY_EDITOR
        /// <summary>
        /// (Editor Only) Clears the save requests in the editor.
        /// </summary>
        public void ClearSaveRequestsInEditor()
        {
            _saveQueue.Clear();
            _saveRequestsLoader.Clear();
        }
#endif
    }
}