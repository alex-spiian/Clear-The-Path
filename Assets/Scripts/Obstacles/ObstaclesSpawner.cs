using System.Collections.Generic;
using UnityEngine;

namespace ClearThePath.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        private const int MAX_ATTEMPTS = 100;
        
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private BoxCollider _trackZone;
        [SerializeField] private BoxCollider _leftZone;
        [SerializeField] private BoxCollider _rightZone;
        
        [SerializeField] private float _playerInitialSize;
        [SerializeField] private float _playerShotSizeFactor;
        [SerializeField] private float _safeMargin;
        [SerializeField] private float _minDistanceBetweenObstacles;
        [SerializeField] private int _maxObstaclesOffTrack;

        private readonly List<Vector3> _spawnedPositions = new();

        public void Spawn()
        {
            var maxObstaclesOnTrack = CalculateMaxObstaclesOnTrack();
            
            SpawnObstacles(_trackZone, maxObstaclesOnTrack);
            SpawnObstacles(_leftZone, _maxObstaclesOffTrack);
            SpawnObstacles(_rightZone, _maxObstaclesOffTrack);
        }

        private int CalculateMaxObstaclesOnTrack()
        {
            return Mathf.FloorToInt(
                (_playerInitialSize * (1 - _safeMargin)) / _playerShotSizeFactor);
        }

        private void SpawnObstacles(BoxCollider zone, int maxObstacles)
        {
            for (int i = 0; i < maxObstacles; i++)
            {
                Vector3 spawnPosition;
                var attempts = 0;

                do
                {
                    spawnPosition = GetRandomPositionInArea(zone);
                    attempts++;
                }
                while (!IsPositionValid(spawnPosition) && attempts < MAX_ATTEMPTS);

                if (attempts < MAX_ATTEMPTS)
                {
                    _spawnedPositions.Add(spawnPosition);
                    Instantiate(_obstaclePrefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Не удалось найти подходящее место для препятствия в боковой зоне.");
                }
            }
        }

        private Vector3 GetRandomPositionInArea(BoxCollider area)
        {
            var center = area.bounds.center;
            var size = area.bounds.size;

            var randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            var randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
            var randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

            return new Vector3(randomX, randomY, randomZ);
        }

        private bool IsPositionValid(Vector3 position)
        {
            foreach (var spawnedPosition in _spawnedPositions)
            {
                if (Vector3.Distance(position, spawnedPosition) < _minDistanceBetweenObstacles)
                {
                    return false;
                }
            }
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            if (_trackZone != null)
                Gizmos.DrawCube(_trackZone.bounds.center, _trackZone.bounds.size);

            Gizmos.color = new Color(1, 0, 0, 0.2f);
            if (_leftZone != null)
                Gizmos.DrawCube(_leftZone.bounds.center, _leftZone.bounds.size);

            Gizmos.color = new Color(0, 0, 1, 0.2f);
            if (_rightZone != null)
                Gizmos.DrawCube(_rightZone.bounds.center, _rightZone.bounds.size);
        }
    }
}