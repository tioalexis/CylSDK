using System;
using System.Collections.Generic;
using CylSDK.Core.App;
using UnityEngine;
using AppContext = CylSDK.Core.App.AppContext;

namespace CylSDK.Core.UserStats
{
    /// <summary>
    /// This service manages user statistics data, allowing for reading, writing, and serialization of user stats.
    /// User stats are stored as key-value pairs, where keys are strings and values are doubles.
    /// </summary>
    public class UserStatsService : IService
    {
        private readonly IUserStatsProvider _userStatsProvider;
        private readonly IUserStatsSerializer _userStatsSerializer;
        private readonly Dictionary<string, double> _data = new();

        /// <summary>
        /// Constructs a new instance of the UserStatsService.
        /// </summary>
        /// <param name="userStatsProvider">The provider for user stats data, which can read and write user stats.</param>
        /// <param name="userStatsSerializer">The serializer for user stats data, which handles serialization and deserialization.</param>
        /// <exception cref="ArgumentNullException">Thrown if either the userStatsProvider or userStatsSerializer is null.</exception>
        public UserStatsService(IUserStatsProvider userStatsProvider, IUserStatsSerializer userStatsSerializer)
        {
            _userStatsProvider = userStatsProvider ?? throw new ArgumentNullException(nameof(userStatsProvider));
            _userStatsSerializer = userStatsSerializer ?? throw new ArgumentNullException(nameof(userStatsSerializer));
        }
        
        /// <summary>
        /// Initializes the UserStatsService asynchronously.
        /// </summary>
        /// <param name="context">The application context that provides necessary information for initialization.</param>
        /// <returns>An Awaitable instance that completes when the service is initialized.</returns>
        public Awaitable InitializeAsync(AppContext context)
        {
            // Nothing to initialize for now, but this can be extended later.
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult();
            return completionSource.Awaitable;
        }

        /// <summary>
        /// Notifies the service to clean up resources.
        /// </summary>
        public void Teardown()
        {
            _data.Clear();
        }
        
        /// <summary>
        /// Loads user stats data asynchronously.
        /// </summary>
        /// <returns>A dictionary containing user stats data.</returns>
        public async Awaitable LoadDataAsync()
        {
            _data.Clear();
            
            var data = await _userStatsProvider.ReadDataAsync(_userStatsSerializer);
            if (data == null)
                return;
            
            foreach (var kvp in data)
                _data[kvp.Key] = kvp.Value;
        }

        /// <summary>
        /// Saves the current user stats data asynchronously.
        /// </summary>
        public async Awaitable SaveDataAsync()
        {
            var serializedData = await _userStatsSerializer.SerializeAsync(_data);
            if (string.IsNullOrEmpty(serializedData))
                return;

            await _userStatsProvider.WriteDataAsync(_data, _userStatsSerializer);
        }
        
        /// <summary>
        /// Retrieves a double value from the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The double value associated with the key, or the default value if the key does not exist.</returns>
        public double GetDouble(string key, double defaultValue = 0.0)
        {
            return _data.GetValueOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves an integer value from the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The integer value associated with the key, or the default value if the key does not exist.</returns>
        public int GetInt(string key, int defaultValue = 0)
        {
            return (int) _data.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Retrieves a float value from the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="defaultValue"> The default value to return if the key does not exist.</param>
        /// <returns>The float value associated with the key, or the default value if the key does not exist.</returns>
        public float GetFloat(string key, float defaultValue = 0.0f)
        {
            return (float) _data.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Retrieves a value of type T from the user stats data by key.
        /// This will attempt to cast the value to type T so it might be slow.
        /// Use <see cref="GetDouble"/>, <see cref="GetInt"/>, or <see cref="GetFloat"/> for better performance if you know the type.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <returns>The value associated with the key cast to type T, or the default value if the key does not exist or cannot be cast.</returns>
        public T GetValue<T>(string key, T defaultValue = default)
        {
            if (!_data.TryGetValue(key, out var value)) 
                return defaultValue;
            
            if (value is T typedValue)
                return typedValue;
            
            return defaultValue;
        }

        /// <summary>
        /// Sets a double value in the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="value">The double value to set for the specified key.</param>
        /// <param name="saveAutomatically">If true, the data will be saved automatically after setting the value.</param>
        public void SetValue(string key, double value, bool saveAutomatically = true)
        {
            _data[key] = value;
            
            if (saveAutomatically)
                _ = SaveDataAsync();
        }

        /// <summary>
        /// Sets an integer value in the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="value">The integer value to set for the specified key.</param>
        /// <param name="saveAutomatically">If true, the data will be saved automatically after setting the value.</param>
        public void SetValue(string key, int value, bool saveAutomatically = true)
        {
            _data[key] = value;
            
            if (saveAutomatically)
                _ = SaveDataAsync();
        }

        /// <summary>
        /// Sets a float value in the user stats data by key.
        /// </summary>
        /// <param name="key">The key for the user stats data.</param>
        /// <param name="value">The float value to set for the specified key.</param>
        /// <param name="saveAutomatically"> If true, the data will be saved automatically after setting the value.</param>
        public void SetValue(string key, float value, bool saveAutomatically = true)
        {
            _data[key] = value;
            
            if (saveAutomatically)
                _ = SaveDataAsync();
        }
    }
}