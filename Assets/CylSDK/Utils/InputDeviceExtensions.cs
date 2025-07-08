using UnityEngine.InputSystem;

namespace CylSDK.Utils
{
    public static class InputDeviceExtensions
    {
        public static bool IsMouseOrTouch(this InputDevice device)
        {
            return device is Mouse or Touchscreen;
        }

        public static bool IsKeyboard(this InputDevice device)
        {
            return device is Keyboard;
        }
    }
}