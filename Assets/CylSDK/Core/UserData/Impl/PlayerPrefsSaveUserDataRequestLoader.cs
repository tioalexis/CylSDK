using UnityEngine;

namespace CylSDK.Core.UserData.Impl
{
    /// <summary>
    /// Implementation of ISaveUserDataRequestsLoader that uses PlayerPrefs to save and load user data requests.
    /// </summary>
    public class PlayerPrefsSaveUserDataRequestLoader : ISaveUserDataRequestsLoader
    {
        private const string PlayerPrefsKey = "CylSDK.SaveUserDataRequests";
        
        /// <inheritdoc />
        public void Save(string serializedData)
        {
            Debug.Log($"{PlayerPrefsKey}: {serializedData}");
            PlayerPrefs.SetString(PlayerPrefsKey, serializedData);
            PlayerPrefs.Save();
        }

        /// <inheritdoc />
        public string Load()
        {
            return PlayerPrefs.GetString(PlayerPrefsKey, string.Empty);
        }
        
        /// <inheritdoc />
        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
            PlayerPrefs.Save();
        }
    }
}