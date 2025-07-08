using System.Collections.Generic;

namespace CylSDK.Core.SceneTransition
{
    /// <summary>
    /// Parameters for scene transitions.
    /// Tries to keep allocations to a minimum by using dictionaries for different types of parameters
    /// and only allocating them when needed.
    /// </summary>
    public class SceneTransitionParams
    {
        private Dictionary<string, string> _stringParams;
        private Dictionary<string, int> _intParams;
        private Dictionary<string, float> _floatParams;
        private Dictionary<string, bool> _boolParams;
        private Dictionary<string, object> _objectParams;
        
        /// <summary>
        /// Sets a string parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public void SetString(string key, string value)
        {
            _stringParams ??= new Dictionary<string, string>();
            _stringParams[key] = value;
        }
        
        /// <summary>
        /// Retrieves a string parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The value of the parameter if it exists, otherwise the default value.</returns>
        public string GetString(string key, string defaultValue = null)
        {
            _stringParams ??= new Dictionary<string, string>();
            return _stringParams.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Sets an integer parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public void SetInt(string key, int value)
        {
            _intParams ??= new Dictionary<string, int>();
            _intParams[key] = value;
        }
        
        /// <summary>
        /// Retrieves an integer parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The value of the parameter if it exists, otherwise the default value.</returns>
        public int GetInt(string key, int defaultValue = 0)
        {
            _intParams ??= new Dictionary<string, int>();
            return _intParams.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Sets a float parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public void SetFloat(string key, float value)
        {
            _floatParams ??= new Dictionary<string, float>();
            _floatParams[key] = value;
        }
        
        /// <summary>
        /// Retrieves a float parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The value of the parameter if it exists, otherwise the default value.</returns>
        public float GetFloat(string key, float defaultValue = 0f)
        {
            _floatParams ??= new Dictionary<string, float>();
            return _floatParams.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Retrieves a boolean parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public void SetBool(string key, bool value)
        {
            _boolParams ??= new Dictionary<string, bool>();
            _boolParams[key] = value;
        }
        
        /// <summary>
        /// The retrieves a boolean parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The value of the parameter if it exists, otherwise the default value.</returns>
        public bool GetBool(string key, bool defaultValue = false)
        {
            _boolParams ??= new Dictionary<string, bool>();
            return _boolParams.GetValueOrDefault(key, defaultValue);
        }
        
        /// <summary>
        /// Sets an object parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        public void SetObject(string key, object value)
        {
            _objectParams ??= new Dictionary<string, object>();
            _objectParams[key] = value;
        }
        
        /// <summary>
        /// Retrieves an object parameter for the scene transition.
        /// </summary>
        /// <param name="key">The key for the parameter.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        /// <returns>The value of the parameter if it exists and is of type T, otherwise the default value.</returns>
        public T GetObject<T>(string key, T defaultValue = default)
        {
            _objectParams ??= new Dictionary<string, object>();
            if (_objectParams.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return defaultValue;
        }
    }
}