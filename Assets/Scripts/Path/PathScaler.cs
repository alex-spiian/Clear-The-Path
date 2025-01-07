using UnityEngine;

namespace Path
{
    public class PathScaler : MonoBehaviour
    {
        public void HandlePathScale(float sizeDecrement)
        {
            var currentSize = Mathf.Max(transform.localScale.x - sizeDecrement, 0);
            var newScale = new Vector3(currentSize, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }
}