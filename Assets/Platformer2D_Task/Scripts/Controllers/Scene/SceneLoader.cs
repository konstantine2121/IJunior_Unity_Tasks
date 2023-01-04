using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(EntitiesFactory))]
    public class SceneLoader : MonoBehaviour, IGameController
    {
        [SerializeField] private Transform _patrolWaypointsContainer;
        [SerializeField] private Vector3 _playerSpawnPosition;
        [SerializeField] private Vector3 _gundamSpawnPosition;
        [SerializeField] private EntitiesFactory _entytiesFactory;        

        private Player _player;
        private GUI _gui;

        private void Start()
        {
            _gui = _entytiesFactory.CreateGUI();
            _gui.RegisterController(this);
        }

        public void Restart()
        {
            RemoveEntities();
            SpawnEntities();
            Resume();
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }

        private void SpawnEntities()
        {
            _player = _entytiesFactory.CreatePlayer(_playerSpawnPosition);
            _gui.RegisterPlayer(_player);
            _entytiesFactory.CreateGundam(_gundamSpawnPosition, GetGundamPatrolPoints());
            _entytiesFactory.CreateBoxSpawner();
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