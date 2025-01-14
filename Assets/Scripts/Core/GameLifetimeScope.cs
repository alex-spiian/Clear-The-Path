using ClearThePath.Obstacles;
using Path;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClearThePath.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
        [SerializeField] private PathScaler _pathScaler;
        [SerializeField] private PathChecker _pathChecker;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();

            builder.RegisterInstance(_obstaclesSpawner);
            builder.RegisterInstance(_pathScaler);
            builder.RegisterInstance(_playerSpawner);
            builder.RegisterInstance(_pathChecker);
        }
    }
}