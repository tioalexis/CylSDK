using UnityEngine;

namespace CylSDK.Utils
{
    /// <summary>
    /// Collection of static methods for drawing debug shapes in Unity.
    /// </summary>
    public static class DebugDraw
    {
        /// <summary>
        /// Draws an X shape at the specified center position with the given color, size, and duration.
        /// </summary>
        /// <param name="center">The center position of the X shape.</param>
        /// <param name="color">The color of the X shape.</param>
        /// <param name="size">The size of the X shape. Default is 0.5f.</param>
        /// <param name="duration">The duration for which the X shape will be visible. Default is 0.02f.</param>
        public static void X(Vector3 center, Color color, float size = 0.5f, float duration = 0.02f)
        {
            var topLeft = center + new Vector3(-size, size, 0f);
            var topRight = center + new Vector3(size, size, 0f);
            var bottomLeft = center + new Vector3(-size, -size, 0f);
            var bottomRight = center + new Vector3(size, -size, 0f);
            
            Debug.DrawLine(topLeft, bottomRight, color, duration);
            Debug.DrawLine(bottomLeft, topRight, color, duration);
        }

        /// <summary>
        /// Draws a circle at the specified center position with the given radius, number of segments, color, and duration.
        /// </summary>
        /// <param name="center">The center position of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="segments">The number of segments to approximate the circle. Must be at least 3.</param>
        /// <param name="color">The color of the circle.</param>
        /// <param name="duration">The duration for which the circle will be visible. Default is 0.02f.</param>
        public static void Circle(Vector3 center, float radius, int segments, Color color, float duration = 0.02f)
        {
            if (radius <= 0 || segments < 3)
            {
                Debug.LogError("Invalid parameters for Circle: radius must be > 0 and segments must be >= 3.");
                return;
            }
            
            var angleStep = 360f / segments;
            angleStep *= Mathf.Deg2Rad;
            
            var lineStart = Vector3.zero;
            var lineEnd = Vector3.zero;
            for (var i = 0; i < segments; i++)
            {
                lineStart.x = Mathf.Cos(angleStep * i);
                lineStart.y = Mathf.Sin(angleStep * i);
                
                lineEnd.x = Mathf.Cos(angleStep * (i + 1));
                lineEnd.y = Mathf.Sin(angleStep * (i + 1));
                
                lineStart *= radius;
                lineEnd *= radius;
                
                lineStart += center;
                lineEnd += center;
                
                Debug.DrawLine(lineStart, lineEnd, color, duration);
            }
        }
        
        /// <summary>
        /// Draws a circle cast trajectory from the origin in the specified direction with the given radius and distance.
        /// </summary>
        /// <param name="origin">The starting point of the circle cast.</param>
        /// <param name="direction">The direction in which the circle cast is performed.</param>
        /// <param name="radius">The radius of the circle cast.</param>
        /// <param name="distance">The distance over which the circle cast is performed.</param>
        /// <param name="color">The color of the circle cast trajectory.</param>
        /// <param name="granularity">The number of circles to draw along the trajectory. Default is 10.</param>
        /// <param name="duration">The duration for which each circle will be visible. Default is 0.02f.</param>
        public static void CircleCast(Vector3 origin, Vector2 direction, float radius, float distance, Color color,
            int granularity = 10, float duration = 0.02f)
        {
            if (radius <= 0 || distance <= 0)
            {
                Debug.LogError("Invalid parameters for CircleCast: radius and distance must be > 0.");
                return;
            }
            
            var endPoint = origin + (Vector3)direction.normalized * distance;
            
            for (var i = 0; i < granularity; i++)
            {
                var t = (float)i / (granularity - 1);
                var pointOnTrajectory = Vector2.Lerp(origin, endPoint, t);
                Circle(pointOnTrajectory, radius, 12, Color.yellow, duration);
            }
        }
    }
}