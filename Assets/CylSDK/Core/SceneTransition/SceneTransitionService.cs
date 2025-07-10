using CylSDK.Core.App;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CylSDK.Core.SceneTransition
{
    public class SceneTransitionService : IService
    {
        /// <summary>
        /// The view responsible for displaying the scene transition UI.
        /// </summary>
        public ISceneTransitionView View { get; private set; }

        /// <summary>
        /// Constructs a new instance of the SceneTransitionService.
        /// </summary>
        /// <param name="sceneTransitionView"></param>
        public SceneTransitionService(ISceneTransitionView sceneTransitionView)
        {
            View = sceneTransitionView;
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
            View.Teardown();
            View = null;
        }
        
        /// <summary>
        /// Switches the scene with a loading screen.
        /// </summary>
        /// <param name="sceneName">The name of the scene to switch to.</param>
        /// <param name="appContext">The application context.</param>
        /// <param name="sceneTransitionParams">Additional parameters for the scene transition.</param>
        public async Awaitable SwitchSceneAsync(
            string sceneName, 
            AppContext appContext,
            SceneTransitionParams sceneTransitionParams = null)
        {
            await Awaitable.MainThreadAsync();
            
            if (View != null)
                await View.ShowLoadingScreenAsync();
            
            await SwitchSceneNoLoadingScreenAsync(sceneName, appContext, sceneTransitionParams);
            
            if (View != null)
                await View.HideLoadingScreenAsync();
        }

        /// <summary>
        /// Switches the scene without showing a loading screen.
        /// </summary>
        /// <param name="sceneName">The name of the scene to switch to.</param>
        /// <param name="appContext">The application context.</param>
        /// <param name="sceneTransitionParams">Additional parameters for the scene transition.</param>
        public async Awaitable SwitchSceneNoLoadingScreenAsync(
            string sceneName,
            AppContext appContext,
            SceneTransitionParams sceneTransitionParams = null)
        {
            await Awaitable.MainThreadAsync();
            
            // Tell the scene controller about the scene exit
            var srcSceneController = Object.FindFirstObjectByType<SceneController>();
            if (srcSceneController != null)
                await srcSceneController.OnSceneExitedAsync(sceneTransitionParams);
            
            // Load the scene asynchronously
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            if (asyncOperation == null)
            {
                Debug.LogError($"Failed to load scene '{sceneName}'. SceneManager returned null.");
                if (View != null)
                    await View.HideLoadingScreenAsync();
                return;
            }
            
            await asyncOperation;
            
            // Tell the scene controller about the scene entry
            var destSceneController = Object.FindFirstObjectByType<SceneController>();
            if (destSceneController != null)
                await destSceneController.OnSceneEnteredAsync(appContext, sceneTransitionParams);
        }
    }
}