using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D_Task
{
    public class EntitiesFactory : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Gundam _gundamPrefab;
        [SerializeField] private Transform _guiPrefab;

        private bool CanCreatePlayer => _playerPrefab != null;

        private bool CanCreateGundam => _gundamPrefab != null;

        private bool CanCreateGUI => _guiPrefab != null;

        public Player CreatePlayer(Vector3 position)
        {
            if (CanCreatePlayer)
            {
                return Instantiate(_playerPrefab, position, Quaternion.identity);
            }

            return null;
        }

        public Gundam CreateGundam(Vector3 position, IEnumerable <Vector3> waypoints)
        {
            if (CanCreateGundam == false)
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

        public Transform CreateGUI()
        {
            if (CanCreateGUI)
            {
                return Instantiate(_guiPrefab, Vector3.zero, Quaternion.identity);
            }

            return null;
        }
    }
}