using ClearThePath.Infectable;
using ClearThePath.Obstacles;
using UnityEngine;

namespace ClearThePath.Core
{
    public class Projectile : InfectableEntity
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _infectionScaleFactor;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Vector3 _targetPosition;
        private float _currentSize;

        public void Initialize()
        {
            _currentSize = 0;
            UpdateScale();
        }

        public void AddSize(float sizeIncrement)
        {
            _currentSize += sizeIncrement;
            UpdateScale();
        }

        public void Launch()
        {
            _rigidbody.isKinematic = false;
            var targetPositionZ = transform.position.z + 20;
            var targetPosition = new Vector3(transform.position.x, transform.position.y, targetPositionZ);
            var direction = targetPosition.normalized;
            _rigidbody.velocity = direction * _speed;
        }

        private void UpdateScale()
        {
            transform.localScale = Vector3.one * _currentSize;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Obstacle>(out var obstacle))
            {
                InfectNearbyObstacles(_currentSize * _infectionScaleFactor);
                Destroy(gameObject);
            }
        }
    }
}