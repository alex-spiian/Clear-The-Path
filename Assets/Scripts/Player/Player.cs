using System;
using ClearThePath.Core;
using UnityEngine;

namespace ClearThePath
{
    public class Player : MonoBehaviour
    {
        public event Action<float> SizeChanging;
        public event Action ObstacleDestroyed;
        public event Action Lost;

        [field:SerializeField] public PlayerMover PlayerMover { get; private set; }
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _launchProjectilePoint;
        [SerializeField] private float _minSize;
        [SerializeField] private float _growthSpeed;

        private float _currentSize;
        private Projectile _currentProjectile;
        private bool _isCharging;
        private bool _canShot;

        public void Initialize()
        {
            _currentSize = Vector3.one.x;
            UpdateScale();
            _canShot = true;
        }

        private void Update()
        {
            if (!_canShot)
                return;
                
            if (Input.GetMouseButtonDown(0))
            {
                StartChargingProjectile();
            }

            if (Input.GetMouseButton(0) && _isCharging)
            {
                ChargeProjectile();
            }

            if (Input.GetMouseButtonUp(0))
            {
                TryReleaseProjectile();
            }
        }

        private void StartChargingProjectile()
        {
            if (_currentProjectile != null) return;

            _isCharging = true;
            _currentProjectile = Instantiate(_projectilePrefab, _launchProjectilePoint.position, Quaternion.identity);
            _currentProjectile.Initialize();
        }

        private void ChargeProjectile()
        {
            if (_currentSize <= _minSize)
            {
                _canShot = false;
                Lost?.Invoke();

                ReleaseProjectile();
                _currentProjectile.SetMinSize();
                _currentProjectile = null;
                return;
            }

            var growthAmount = _growthSpeed * Time.deltaTime;
            _currentProjectile.AddSize(growthAmount);
            SizeChanging?.Invoke(growthAmount);
            _currentSize -= growthAmount;
            UpdateScale();
        }

        private void TryReleaseProjectile()
        {
            if (!_isCharging || _currentProjectile == null)
                return;

            ReleaseProjectile();
            _currentProjectile = null;
        }

        private void ReleaseProjectile()
        {
            _isCharging = false;
            _currentProjectile.Launch(ObstacleDestroyed);
            ObstacleDestroyed?.Invoke();
        }

        private void UpdateScale()
        {
            transform.localScale = Vector3.one * _currentSize;
        }
    }
}