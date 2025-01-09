using System;
using System.Threading.Tasks;
using ClearThePath;
using UnityEngine;

namespace Path
{
    public class PathChecker : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private int _delayBeforeCheckingMs;
        [SerializeField] private LayerMask _checkLayer;

        private Action _pathClearedCallBack;
        
        public void Initialize(Action pathClearedCallBack)
        {
            _pathClearedCallBack = pathClearedCallBack;
        }
   
        private void Win()
        {
            _pathClearedCallBack?.Invoke();
        }

        public async void CheckPath()
        {
            await Task.Delay(_delayBeforeCheckingMs);
            CheckAllCollisions();
        }

        private void CheckAllCollisions()
        {
            var boxCenter = _collider.bounds.center;
            var boxSize = _collider.bounds.size / 2;
            var hitColliders = Physics.OverlapBox(boxCenter, boxSize, Quaternion.identity, _checkLayer);

            if (hitColliders.Length == 0)
            {
                Win();
                return;
            }

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag(GlobalConstants.OBSTACLE_TAG))
                {
                    return;
                }
            }
            
            Win();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_collider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
            }
        }
#endif
    }
}