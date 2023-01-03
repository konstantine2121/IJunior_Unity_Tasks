using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace Platformer2D_Task
{
    public class EntitiesFactory : MonoBehaviour
    {
        private const string PlayerPrefab = "Player.prefab";
        private const string GundamPrefab = "Gundam.prefab";
        private const string GuiPrefab = "GUI.prefab";
        private const string BoxSpawnerPrefab = "BoxSpawner.prefab";

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
            _playerPrefab = AssetDatabase.LoadAssetAtPath<Player>(Path.Combine(ResourcesPaths.PrefabsDirPath, PlayerPrefab));
            _gundamPrefab = AssetDatabase.LoadAssetAtPath<Gundam>(Path.Combine(ResourcesPaths.PrefabsDirPath, GundamPrefab));
            _guiPrefab = AssetDatabase.LoadAssetAtPath<GUI>(Path.Combine(ResourcesPaths.PrefabsDirPath, GuiPrefab));
            _boxSpawnerPrefab = AssetDatabase.LoadAssetAtPath<BoxSpawner>(Path.Combine(ResourcesPaths.PrefabsDirPath, BoxSpawnerPrefab));
        }
    }
}