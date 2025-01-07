using ClearThePath.Core;
using UnityEngine;

namespace ClearThePath
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _launchProjectilePoint;
        [SerializeField] private float _initialSize;
        [SerializeField] private float _minSize;
        [SerializeField] private float _growthSpeed;

        private float _currentSize;
        private Projectile _currentProjectile;
        private bool _isCharging;

        private void Start()
        {
            _currentSize = _initialSize;
            UpdateScale();
        }

        private void Update()
        {
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
                ReleaseProjectile();
            }
        }

        private void StartChargingProjectile()
        {
            if (_currentProjectile != null) return;

            _isCharging = true;

            _currentProjectile = Instantiate(_projectilePrefab, _launchProjectilePoint.position, Quaternion.identity);
            _currentProjectile.Initialize(0);
        }

        private void ChargeProjectile()
        {
            if (_currentSize <= _minSize)
            {
                GameManager.Instance.FailGame();
                return;
            }

            var growthAmount = _growthSpeed * Time.deltaTime;
            _currentProjectile.AddSize(growthAmount);

            _currentSize -= growthAmount;
            UpdateScale();
        }

        private void ReleaseProjectile()
        {
            if (!_isCharging || _currentProjectile == null)
                return;
            
            _isCharging = false;
            _currentProjectile.Launch();
            _currentProjectile = null;
        }

        private void UpdateScale()
        {
            transform.localScale = Vector3.one * _currentSize;
        }
    }
}