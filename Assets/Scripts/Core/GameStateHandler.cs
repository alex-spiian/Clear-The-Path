using System;
using Path;
using UnityEngine;

namespace ClearThePath.Core
{
    public class GameStateHandler : IDisposable
    {
        private Player _player;
        private PathChecker _pathChecker;
        private PlayerMover _playerMover;

        public void Initialize(Player player, PathChecker pathChecker)
        {
            _pathChecker = pathChecker;
            _player = player;
            _playerMover = _player.PlayerMover;
            
            _pathChecker.Initialize(OnWin);
            _player.Lost += OnLost;
        }
        
        private void OnWin()
        {
            _playerMover.Move(_pathChecker.StartMovingPoint, _pathChecker.ExitPointPoint, OnPlayerGotExit);
        }

        private void OnPlayerGotExit()
        {
            Debug.Log("you got finish");
        }

        private void OnLost()
        {
            Debug.Log("you lost");
        }

        public void Dispose()
        {
            _player.Lost -= OnLost;
        }
    }
}