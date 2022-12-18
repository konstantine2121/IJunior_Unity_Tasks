using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D_Task
{
    public class EntitiesFactory : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Gundam _gundamPrefab;

        private bool CanSpawnPlayer => _playerPrefab != null;

        private bool CanSpawnGundam => _gundamPrefab != null;

        public Player SpawnPlayer(Vector3 position)
        {
            if (CanSpawnPlayer)
            {
                return Instantiate(_playerPrefab, position, Quaternion.identity);
            }

            return null;
        }

        public Gundam SpawnGundam(Vector3 position, IEnumerable <Vector3> waypoints)
        {
            if (CanSpawnGundam == false)
            {
                return null;                
            }

            var gundam = Instantiate(_gundamPrefab, position, Quaternion.identity);

            if (waypoints != null)
            {
                var patrol = gundam.GetComponent<GundamPatrol>();
                patrol.SetNewWayPoints(waypoints);
            }

            return gundam;
        }
    }
}