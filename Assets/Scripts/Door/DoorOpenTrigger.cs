using UnityEngine;

namespace ClearThePath.Environment
{
    public class DoorOpenTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                _door.Open();
            }
        }
    }
}