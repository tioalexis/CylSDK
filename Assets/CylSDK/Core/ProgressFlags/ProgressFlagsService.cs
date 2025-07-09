using System.Collections.Generic;
using CylSDK.Core.App;
using UnityEngine;

namespace CylSDK.Core.ProgressFlags
{
    /// <summary>
    /// This service manages progress flags, allowing you to set and get flags that indicate the progress of various
    /// tasks or achievements in your application.
    /// </summary>
    public class ProgressFlagsService : IService
    {
        private readonly Dictionary<string, bool> _progressFlags = new();
        private readonly IProgressFlagsProvider _progressFlagsProvider;
        private readonly IProgressFlagsSerializer _progressFlagsSerializer;

        /// <summary>
        /// Constructs a new instance of the ProgressFlagsService.
        /// </summary>
        /// <param name="progressFlagsProvider">The provider used to read and write progress flags data.</param>
        /// <param name="progressFlagsSerializer">The serializer used to serialize and deserialize progress flags data.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either the provider or serializer is null.</exception>
        public ProgressFlagsService(IProgressFlagsProvider progressFlagsProvider,
            IProgressFlagsSerializer progressFlagsSerializer)
        {
            _progressFlagsProvider = progressFlagsProvider ?? throw new System.ArgumentNullException(nameof(progressFlagsProvider));
            _progressFlagsSerializer = progressFlagsSerializer ?? throw new System.ArgumentNullException(nameof(progressFlagsSerializer));
        }
        
        /// <inheritdoc />
        public Awaitable InitializeAsync(AppContext context)
        {
            var completionSource = new AwaitableCompletionSource();
            completionSource.SetResult();
            return completionSource.Awaitable;
        }

        /// <inheritdoc />
        public void Teardown()
        {
            _progressFlags.Clear();
        }
        
        /// <summary>
        /// Loads the progress flags data asynchronously.
        /// </summary>
        /// <returns>An Awaitable containing a dictionary where each key is a progress flag and the value is true if the flag was set.</returns>
        public async Awaitable<Dictionary<string, bool>> LoadDataAsync()
        {
            var data = await _progressFlagsProvider.ReadDataAsync(_progressFlagsSerializer);
            
            _progressFlags.Clear();
            foreach (var kvp in data)
                _progressFlags[kvp.Key] = kvp.Value;
            
            return _progressFlags;
        }
        
        /// <summary>
        /// Saves the current progress flags data asynchronously.
        /// </summary>
        public async Awaitable SaveDataAsync()
        {
            await _progressFlagsProvider.WriteDataAsync(_progressFlags, _progressFlagsSerializer);
        }
        
        /// <summary>
        /// Retrieves the value of a progress flag by its key.
        /// If the flag does not exist, it returns the specified default value (false by default).
        /// </summary>
        /// <param name="key">The key of the progress flag to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the flag is not set. Defaults to false.</param>
        /// <returns>True if the flag is set, otherwise false.</returns>
        public bool GetFlag(string key, bool defaultValue = false)
        {
            return _progressFlags.GetValueOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Sets the value of a progress flag by its key.
        /// </summary>
        /// <param name="key">The key of the progress flag to set.</param>
        /// <param name="value">The value to set for the progress flag.</param>
        /// <param name="saveAutomatically">The flag indicating whether to automatically save the data after setting the flag. Defaults to false.</param>
        public void SetFlag(string key, bool value, bool saveAutomatically = false)
        {
            _progressFlags[key] = value;
            
            if (saveAutomatically)
                _ = SaveDataAsync();
        }
    }
}