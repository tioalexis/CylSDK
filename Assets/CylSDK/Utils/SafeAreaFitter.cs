using UnityEngine;

namespace CylSDK.Utils
{
    /// <summary>
    /// Automatically adjusts the RectTransform of a UI element to fit within the safe area of the screen.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        [SerializeField] private bool fitHorizontal = true;
        [SerializeField] private bool fitVertical = true;
        
        private RectTransform _rectTransform;
        private Vector2 _originalAnchorMin;
        private Vector2 _originalAnchorMax;
        private Vector2 _originalSizeDelta;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalAnchorMin = _rectTransform.anchorMin;
            _originalAnchorMax = _rectTransform.anchorMax;
            _originalSizeDelta = _rectTransform.sizeDelta;
            
            Apply();
        }

        private void Apply()
        {
            var safeArea = Screen.safeArea;
            var anchorMin = _rectTransform.anchorMin;
            var anchorMax = _rectTransform.anchorMax;
            var sizeDelta = _rectTransform.sizeDelta;
            if (fitHorizontal)
            {
                anchorMin.x = safeArea.xMin / Screen.width;
                anchorMax.x = safeArea.xMax / Screen.width;
                sizeDelta.x = 0; // Reset width to fit the safe area
            }
            else
            {
                anchorMin.x = _originalAnchorMin.x;
                anchorMax.x = _originalAnchorMax.x;
                sizeDelta.x = _originalSizeDelta.x; // Keep original width
            }
            
            if (fitVertical)
            {
                anchorMin.y = safeArea.yMin / Screen.height;
                anchorMax.y = safeArea.yMax / Screen.height;
                sizeDelta.y = 0; // Reset height to fit the safe area
            }
            else
            {
                anchorMin.y = _originalAnchorMin.y;
                anchorMax.y = _originalAnchorMax.y;
                sizeDelta.y = _originalSizeDelta.y; // Keep original height
            }
            
            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
            _rectTransform.sizeDelta = sizeDelta;
        }
    }
}