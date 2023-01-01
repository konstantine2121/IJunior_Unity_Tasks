using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

namespace Platformer2D_Task
{
    public class EntitiesFactory : MonoBehaviour
    {
        private const string PlayerPrefab = "Player.prefab";
        private const string GundamPrefab = "Gundam.prefab";
        private const string GuiPrefab = "GUI.prefab";

        private Player _playerPrefab;
        private Gundam _gundamPrefab;
        private GUI _guiPrefab;

        private Player _player;

        private bool CanCreatePlayer => _playerPrefab != null;

        private bool CanCreateGundam => _gundamPrefab != null;

        private bool CanCreateGUI => _guiPrefab != null;
        
        private void Awake()
        {
            GetLinks();
        }

        public Player CreatePlayer(Vector3 position)
        {
            if (CanCreatePlayer)
            {
                _player = Instantiate(_playerPrefab, position, Quaternion.identity);
                return _player;
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
                if (_player == null)
                {
                    throw new InvalidOperationException("Сущность player должна быть создана до создания интерфейсов.");
                }

                var gui = Instantiate(_guiPrefab, Vector3.zero, Quaternion.identity);

                gui.PlayerHPBar.RegisterHealth(_player.Health);
                gui.ScoreBar.RegisterCollector(_player.BoxCollector);

                return gui;
            }

            return null;
        }

        private void GetLinks()
        {
            _playerPrefab = AssetDatabase.LoadAssetAtPath<Player>(Path.Combine(ResourcesPaths.PrefabsDirPath, PlayerPrefab));
            _gundamPrefab = AssetDatabase.LoadAssetAtPath<Gundam>(Path.Combine(ResourcesPaths.PrefabsDirPath, GundamPrefab));
            _guiPrefab = AssetDatabase.LoadAssetAtPath<GUI>(Path.Combine(ResourcesPaths.PrefabsDirPath, GuiPrefab));
        }
    }
}