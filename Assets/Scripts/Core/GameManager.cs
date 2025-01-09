using System;
using ClearThePath.Obstacles;
using Path;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClearThePath.Core
{
    public class GameManager : IStartable, IDisposable
    {
        private PlayerSpawner _playerSpawner;
        private ObstaclesSpawner _obstaclesSpawner;
        private PathScaler _pathScaler;
        private Player _player;

        [Inject]
        public void Construct(PlayerSpawner playerSpawner, ObstaclesSpawner obstaclesSpawner, PathScaler pathController)
        {
            _playerSpawner = playerSpawner;
            _obstaclesSpawner = obstaclesSpawner;
            _pathScaler = pathController;
        }
        
        public void Start()
        {
            StartGame();
        }

        private void StartGame()
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
        
        public void Dispose()
        {
            _player.SizeChanging -= _pathScaler.HandlePathScale;
            _player.Lost -= OnLost;
        }
    }
}