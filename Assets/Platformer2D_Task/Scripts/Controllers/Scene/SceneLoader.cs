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
        private GUI _ui;
        private PlayerHPBar _playerHP;
        private ScoreBar _score;
        private EntitiesFactory _entytiesFactory;

        public void Restart()
        {
            UnbindUI();
            RemoveEntities();
            SpawnEntities();
            BindUI();
        }

        private void Start()
        {
            _entytiesFactory = GetComponent<EntitiesFactory>();
            CreateUI();
            Restart();            
        }

        private void CreateUI()
        {
            _ui = _entytiesFactory.CreateGUI();

            _playerHP = _ui?.GetComponentsInChildren<PlayerHPBar>().FirstOrDefault();
            _score = _ui?.GetComponentsInChildren<ScoreBar>().FirstOrDefault();
        }

        private void BindUI()
        {
            if (_player == null)
            {
                return;
            }

            _playerHP?.RegisterHealth(_player.Health);
            _score?.RegisterCollector(_player.BoxCollector);
        }

        private void UnbindUI()
        {
            if (_player == null)
            {
                return;
            }

            _playerHP?.UnregisterHealth();
            _score?.UnregisterCollector();
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