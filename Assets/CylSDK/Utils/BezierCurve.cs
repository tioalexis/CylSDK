using UnityEngine;

namespace CylSDK.Utils
{
    /// <summary>
    /// A utility class for working with Bézier curves.
    /// </summary>
    public static class BezierCurve
    {
        /// <summary>
        /// Calculates a point on a cubic Bézier curve defined by the start point, one control point, and the end point.
        /// </summary>
        /// <param name="p0">The start of the curve.</param>
        /// <param name="p1">The middle control point that influences the curve's shape.</param>
        /// <param name="p2">The end of the curve.</param>
        /// <param name="t">The parameter that determines the position along the curve, where 0 is the start and 1 is the end.</param>
        /// <returns>A point on the cubic Bézier curve at parameter t.</returns>
        public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            var u = 1 - t;
            return u * u * p0 + 2 * u * t * p1 + t * t * p2;
        }
    }
}