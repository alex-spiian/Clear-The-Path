using ClearThePath.Obstacles;
using Path;
using UnityEngine;

namespace ClearThePath.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
        [SerializeField] private PathScaler _pathScaler;
        
        private Player _player;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _player = _playerSpawner.Spawn();
            _player.Initialize();
            _obstaclesSpawner.Spawn();
            
            _player.SizeChanging += _pathScaler.HandlePathScale;
            _player.Lost += OnLost;
        }

        private void OnLost()
        {
            Debug.Log("you lost");
        }

        private void OnDestroy()
        {
            _player.SizeChanging -= _pathScaler.HandlePathScale;
            _player.Lost -= OnLost;
        }
    }
}