using UnityEngine.InputSystem;

namespace CylSDK.Utils
{
    /// <summary>
    /// Collection of static methods for working with InputDevice types in Unity.
    /// </summary>
    public static class InputDeviceExtensions
    {
        /// <summary>
        /// Checks if the given InputDevice is a mouse or touchscreen.
        /// </summary>
        /// <param name="device">The InputDevice to check.</param>
        /// <returns>True if the device is a Mouse or Touchscreen, otherwise false.</returns>
        public static bool IsMouseOrTouch(this InputDevice device)
        {
            return device is Mouse or Touchscreen;
        }

        /// <summary>
        /// Checks if the given InputDevice is a keyboard.
        /// </summary>
        /// <param name="device">The InputDevice to check.</param>
        /// <returns>True if the device is a Keyboard, otherwise false.</returns>
        public static bool IsKeyboard(this InputDevice device)
        {
            return device is Keyboard;
        }
    }
}