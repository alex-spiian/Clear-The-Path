using UnityEngine;

namespace ClearThePath
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnPoint;

        public Player Spawn()
        {
            return Instantiate(_playerPrefab, _spawnPoint);
        }
    }
}