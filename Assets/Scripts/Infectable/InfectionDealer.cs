using ClearThePath.Obstacles;
using UnityEngine;

namespace ClearThePath.Infectable
{
    public abstract class InfectionDealer : MonoBehaviour
    {
        protected void InfectNearbyObstacles(float infectionRadius)
        {
            var nearbyColliders = Physics.OverlapSphere(transform.position, infectionRadius);
            foreach (var collider in nearbyColliders)
            {
                if (collider.TryGetComponent(out Obstacle nearbyObstacle))
                {
                    nearbyObstacle.Infect();
                }
            }
        }
    }
}