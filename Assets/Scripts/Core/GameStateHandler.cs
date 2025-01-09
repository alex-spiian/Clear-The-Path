using System;
using Path;
using UnityEngine;

namespace ClearThePath.Core
{
    public class GameStateHandler : IDisposable
    {
        private Player _player;
        private PathChecker _pathChecker;

        public void Initialize(Player player, PathChecker pathChecker)
        {
            _pathChecker = pathChecker;
            _player = player;
            
            _pathChecker.Initialize(OnWin);
            _player.Lost += OnLost;
        }
        
        private void OnWin()
        {
            Debug.Log("you win");
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