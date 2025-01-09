using ClearThePath.Infectable;
using UnityEngine;

namespace ClearThePath.Obstacles
{
    public class Obstacle : InfectionDealer
    {
        [SerializeField] private float _infectionRadius;
        [SerializeField] private float _explosionDelay;
        [SerializeField] private Color _infectedColor;
        [SerializeField] private Renderer _renderer;

        private Material _materialInstance;
        private bool _isInfected;

        private void Awake()
        {
            if (_renderer != null)
            {
                _materialInstance = _renderer.material;
            }
            else
            {
                Debug.LogError("Renderer is not assigned on the Obstacle object!");
            }
        }

        public void Infect()
        {
            if (_isInfected)
                return;

            _isInfected = true;

            if (_materialInstance != null)
            {
                _materialInstance.color = _infectedColor;
            }

            InfectNearbyObstacles(_infectionRadius);
            Invoke(nameof(Explode), _explosionDelay);
        }

        private void Explode()
        {
            Destroy(gameObject);
        }
    }
}