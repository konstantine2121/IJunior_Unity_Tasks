using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(EntitiesFactory))]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Transform _patrolWaypointsContainer;
        [SerializeField] private Vector3 _playerSpawnPosition;
        [SerializeField] private Vector3 _gundamSpawnPosition;
        private Player _player;

        private Transform _ui;
        private PlayerHPBar _playerHP;

        private EntitiesFactory _entytiesFactory;

        public void Restart()
        {
            UnbindUI();
            RemoveEntities();
            SpawnEntities();
            BindUI();
        }

        private void Awake()
        {
            _entytiesFactory = GetComponent<EntitiesFactory>();
            CreateUI();
            Restart();            
        }

        private void CreateUI()
        {
            _ui = _entytiesFactory.CreateGUI();

            _playerHP = _ui?.GetComponentsInChildren<PlayerHPBar>().FirstOrDefault();
        }

        private void BindUI()
        {
            if (_player == null)
            {
                return;
            }

            var health = _player.GetComponent<Health>();
            var collector = _player.GetComponent<BoxCollector>();

            _playerHP.RegisterHealth(health);

        }

        private void UnbindUI()
        {
            if (_player == null)
            {
                return;
            }

            _playerHP?.UnregisterHealth();

            
            var collector = _player.GetComponent<BoxCollector>();

            
        }

        private void SpawnEntities()
        {
            _player = _entytiesFactory.CreatePlayer(_playerSpawnPosition);

            _entytiesFactory.CreateGundam(_gundamSpawnPosition, GetGundamPatrolPoints());
        }

        private void RemoveEntities()
        {
            RemoveEntitiesOfType<Player>();
            RemoveEntitiesOfType<Gundam>();
            RemoveEntitiesOfType<Box>();
        }

        private void RemoveEntitiesOfType<T>() where T : MonoBehaviour
        {
            var objects = FindObjectsOfType(typeof(T)) as MonoBehaviour[];

            if (objects == null)
            {
                return;
            }

            foreach(var obj in objects)
            {   
                Destroy(obj.gameObject);
            }
        }

        private List<Vector3> GetGundamPatrolPoints()
        {
            var points = new List<Vector3>();

            if (_patrolWaypointsContainer != null)
            {
                var childs = _patrolWaypointsContainer.GetComponentsInChildren<Transform>()
                    .Where(component => component != _patrolWaypointsContainer);

                foreach(var child in childs)
                {
                    points.Add(child.position);
                }
            }

            return points;
        }
    }
}