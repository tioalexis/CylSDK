using CylSDK.Core.App;
using UnityEngine;

namespace CylSDK.Core.SceneTransition
{
    /// <summary>
    /// Base class for scene controllers that handle scene transitions.
    /// </summary>
    public abstract class SceneController : MonoBehaviour
    {
        /// <summary>
        /// Invoked when the scene is loaded by the SceneTransitionService.
        /// </summary>
        /// <param name="appContext"></param>
        /// <param name="sceneTransitionParams"></param>
        /// <returns></returns>
        public abstract Awaitable OnSceneEnteredAsync(AppContext appContext, SceneTransitionParams sceneTransitionParams);
        
        /// <summary>
        /// Invoked when the scene is exited by the SceneTransitionService.
        /// </summary>
        /// <param name="sceneTransitionParams"></param>
        /// <returns></returns>
        public abstract Awaitable OnSceneExitedAsync(SceneTransitionParams sceneTransitionParams);
    }
}