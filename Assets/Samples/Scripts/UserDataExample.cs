using System;
using CylSDK.Core.UserData;
using CylSDK.Core.UserData.Impl;
using CylSDK.Utils;
using TMPro;
using UnityEngine;
using AppContext = CylSDK.Core.App.AppContext;
using Random = UnityEngine.Random;

namespace Samples.Scripts
{
    public class MyUserDataModel : IUserDataModel
    {
        public string UserName { get; set; }
        public int UserScore { get; set; }
    }
    
    public class MyUserDataProvider : IUserDataProvider
    {
        private const string UserNameKey = "CylSDK.UserDataExample.UserName";
        private const string UserScoreKey = "CylSDK.UserDataExample.UserScore";
        
        private const string DefaultUserName = "DefaultUser";
        private const int DefaultUserScore = 0;
        
        public Awaitable<IUserDataModel> CreateUserDataAsync(IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<IUserDataModel>();
            var userData = new MyUserDataModel
            {
                UserName = DefaultUserName,
                UserScore = DefaultUserScore
            };
            completionSource.SetResult(userData); // Simulate creating user data
            return completionSource.Awaitable;
        }

        public Awaitable<IUserDataModel> ReadUserDataAsync(IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource<IUserDataModel>();
            var userData = new MyUserDataModel
            {
                UserName = PlayerPrefs.GetString(UserNameKey, DefaultUserName),
                UserScore = PlayerPrefs.GetInt(UserScoreKey, DefaultUserScore)
            };
            completionSource.SetResult(userData); // Simulate reading user data
            return completionSource.Awaitable;
        }

        public Awaitable WriteUserDataAsync(IUserDataModel userData, IUserDataSerializer serializer)
        {
            var completionSource = new AwaitableCompletionSource();
            if (userData is MyUserDataModel myUserData)
            {
                PlayerPrefs.SetString(UserNameKey, myUserData.UserName);
                PlayerPrefs.SetInt(UserScoreKey, myUserData.UserScore);
                PlayerPrefs.Save();
            }
            completionSource.SetResult(); // Simulate writing user data
            return completionSource.Awaitable;
        }
    }
    
    public class UserDataExample : MonoBehaviour
    {
        [SerializeField] private TMP_Text userNameText;
        [SerializeField] private TMP_Text userScoreText;
        
        private AppContext _context;
        private UserDataService<MyUserDataModel> _userDataService;

        private void Awake()
        {
            _context = new AppContext();
            
            var userDataSerializer = new JsonUserDataSerializer();
            var userDataProvider = new MyUserDataProvider();
            _userDataService = new UserDataService<MyUserDataModel>(userDataProvider, userDataSerializer);
            _context.ServiceLocator.RegisterService(_userDataService);
        }

        private async void Start()
        {
            try
            {
                LoadUserData();

                await _userDataService.InitializeAsync(_context);
            
                InvokeRepeating(nameof(UpdateDisplayedData), 0f, 1f/8f); // Update every 1/8 second
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize UserDataService: {e.Message}");
            }
        }

        private void OnDestroy()
        {
            _context.Teardown();
        }

        public void AddRandomScore()
        {
            _userDataService.UserData.UserScore += Random.Range(1, 100);
        }

        public void SaveUserData()
        {
            _userDataService.SaveUserData();
        }
        
        public void LoadUserData()
        {
            _userDataService
                .LoadUserDataAsync()
                .FireAndForget();
        }
        
        public void SpamSaveRequests(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _userDataService.SaveUserData();
            }
        }

        public void ClearPlayerPrefs()
        {
#if UNITY_EDITOR
            _userDataService.ClearSaveRequestsInEditor();
#endif
            
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        private void UpdateDisplayedData()
        {
            userNameText.text = $"User: {_userDataService.UserData.UserName}";
            userScoreText.text = $"Score: {_userDataService.UserData.UserScore}";
        }
        
    }
}