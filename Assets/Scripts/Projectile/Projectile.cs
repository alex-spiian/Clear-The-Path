using System;
using ClearThePath.Infectable;
using ClearThePath.Obstacles;
using UnityEngine;

namespace ClearThePath.Core
{
    public class Projectile : InfectionDealer
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _infectionScaleFactor;
        [SerializeField] private float _minProjectileSize;
        [SerializeField] private float _maxProjectileSize;
        [SerializeField] private float _distanceToTravel;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Vector3 _targetPosition;
        private float _currentSize;
        private event Action _onObstacleDestroyed;

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

        public void SetMinSize()
        {
            _currentSize = _minProjectileSize;
            UpdateScale();
        }

        public void Launch(Action OnObstacleDestroyed)
        {
            CorrectSize();
            _onObstacleDestroyed = OnObstacleDestroyed;
            _rigidbody.isKinematic = false;
            var targetPositionZ = transform.position.z + _distanceToTravel;
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
                _onObstacleDestroyed?.Invoke();
            }
        }

        private void CorrectSize()
        {
            _currentSize = Mathf.Clamp(_currentSize, _minProjectileSize, _maxProjectileSize);
        }
    }
}