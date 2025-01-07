using ClearThePath.Infectable;
using UnityEngine;

namespace ClearThePath.Obstacles
{
    public class Obstacle : InfectableEntity
    {
        [SerializeField] private float _infectionRadius;
        private bool _isInfected;

        public void Infect()
        {
            if (_isInfected)
                return;

            _isInfected = true;
            InfectNearbyObstacles(_infectionRadius);
            Explode();
        }

        private void Explode()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _infectionRadius);
        }
    }
}