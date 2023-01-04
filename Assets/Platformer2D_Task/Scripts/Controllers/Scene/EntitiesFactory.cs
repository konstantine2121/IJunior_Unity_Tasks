using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D_Task
{
    public class EntitiesFactory : MonoBehaviour
    {
        private const string PlayerPrefab = "Player";
        private const string GundamPrefab = "Gundam";
        private const string GuiPrefab = "GUI";
        private const string BoxSpawnerPrefab = "BoxSpawner";

        private Player _playerPrefab;
        private Gundam _gundamPrefab;
        private GUI _guiPrefab;
        private BoxSpawner _boxSpawnerPrefab;

        private bool CanCreatePlayer => _playerPrefab != null;

        private bool CanCreateGundam => _gundamPrefab != null;

        private bool CanCreateGUI => _guiPrefab != null;

        private bool CanCreateBoxSpawner => _boxSpawnerPrefab != null;
                
        private void OnEnable()
        {
            GetLinks();
        }

        public Player CreatePlayer(Vector3 position)
        {
            if (CanCreatePlayer)
            {
                return Instantiate(_playerPrefab, position, Quaternion.identity);
            }

            return null;
        }

        public BoxSpawner CreateBoxSpawner()
        {
            if (CanCreateBoxSpawner)
            {
                return Instantiate(_boxSpawnerPrefab);
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

        public GUI CreateGUI()
        {
            if (CanCreateGUI)
            {
                return Instantiate(_guiPrefab, Vector3.zero, Quaternion.identity);
            }

            return null;
        }

        private void GetLinks()
        {
            _playerPrefab = Resources.Load<Player>(PlayerPrefab);
            _gundamPrefab = Resources.Load<Gundam>(GundamPrefab);
            _guiPrefab = Resources.Load<GUI>(GuiPrefab);
            _boxSpawnerPrefab = Resources.Load<BoxSpawner>(BoxSpawnerPrefab);
        }
    }
}