using UnityEngine;

namespace CylSDK.Core.SceneTransition
{
    /// <summary>
    /// Interface for scene transition views that handle loading screens.
    /// </summary>
    public interface ISceneTransitionView
    {
        /// <summary>
        /// Shows the loading screen asynchronously.
        /// </summary>
        /// <returns>An awaitable task that completes when the loading screen is shown.</returns>
        Awaitable ShowLoadingScreenAsync();
        
        /// <summary>
        /// Hides the loading screen asynchronously.
        /// </summary>
        /// <returns>An awaitable task that completes when the loading screen is hidden.</returns>
        Awaitable HideLoadingScreenAsync();

        /// <summary>
        /// Shows the loading screen immediately without waiting for any animations or delays.
        /// </summary>
        void ShowLoadingScreenImmediate();

        /// <summary>
        /// Invoked when the SceneTransitionService is destroyed or the application is quitting.
        /// </summary>
        void Teardown();
    }
}