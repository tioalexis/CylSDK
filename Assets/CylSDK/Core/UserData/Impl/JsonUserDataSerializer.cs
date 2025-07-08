using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// Serializer for user data models using JSON format.
    /// Uses Newtonsoft.Json for serialization and deserialization.
    /// </summary>
    public class JsonUserDataSerializer : IUserDataSerializer
    {
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when userData is null.</exception>
        public string Serialize(IUserDataModel userData)
        {
            if (userData == null)
            {
                throw new ArgumentNullException(nameof(userData), "User data cannot be null.");
            }

            try
            {
                return JsonConvert.SerializeObject(userData);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown when serializedData is null or empty.</exception>
        public T Deserialize<T>(string serializedData) where T : IUserDataModel
        {
            if (string.IsNullOrEmpty(serializedData))
            {
                throw new ArgumentNullException(nameof(serializedData), "Serialized data cannot be null or empty.");
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(serializedData);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }
    }
}