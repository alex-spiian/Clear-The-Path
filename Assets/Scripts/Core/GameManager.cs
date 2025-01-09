using System;
using ClearThePath.Obstacles;
using Path;
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
        private PathChecker _pathChecker;
        private GameStateHandler _gameStateHandler;

        [Inject]
        public void Construct(PlayerSpawner playerSpawner,
            ObstaclesSpawner obstaclesSpawner,
            PathScaler pathController,
            PathChecker pathChecker)
        {
            _pathChecker = pathChecker;
            _playerSpawner = playerSpawner;
            _obstaclesSpawner = obstaclesSpawner;
            _pathScaler = pathController;

            _gameStateHandler = new GameStateHandler();
        }
        
        public void Start()
        {
            Initialize();
        }
        
        public void Dispose()
        {
            _player.SizeChanging -= _pathScaler.HandlePathScale;
            _player.ObstacleDestroyed -= _pathChecker.CheckPath;
            _gameStateHandler.Dispose();
        }

        private void Initialize()
        {
            _player = _playerSpawner.Spawn();
            _obstaclesSpawner.Spawn();
            
            _player.Initialize();
            _gameStateHandler.Initialize(_player, _pathChecker);

            _player.SizeChanging += _pathScaler.HandlePathScale;
            _player.ObstacleDestroyed += _pathChecker.CheckPath;
        }
    }
}