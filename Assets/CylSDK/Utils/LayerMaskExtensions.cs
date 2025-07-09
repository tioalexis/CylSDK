using UnityEngine;

namespace CylSDK.Utils
{
    /// <summary>
    /// Collection of static methods for working with LayerMask in Unity.
    /// </summary>
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Checks if the specified LayerMask contains the given layer.
        /// </summary>
        /// <param name="layerMask">The LayerMask to check against.</param>
        /// <param name="layer">The layer to check for.</param>
        /// <returns>True if the LayerMask contains the specified layer, otherwise false.</returns>
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return (layerMask & (1 << layer)) != 0;
        }
    }
}