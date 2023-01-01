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

        private EntitiesFactory _entytiesFactory;

        public void Restart()
        {
            RemoveEntities();
            SpawnEntities();
            CreateUI();
        }

        private void Start()
        {
            _entytiesFactory = GetComponent<EntitiesFactory>();
            Restart();            
        }

        private void CreateUI()
        {
            _entytiesFactory.CreateGUI();
        }

        private void SpawnEntities()
        {
            _entytiesFactory.CreatePlayer(_playerSpawnPosition);
            _entytiesFactory.CreateGundam(_gundamSpawnPosition, GetGundamPatrolPoints());
        }

        private void RemoveEntities()
        {
            RemoveEntitiesOfType<Player>();
            RemoveEntitiesOfType<Gundam>();
            RemoveEntitiesOfType<Box>();
            RemoveEntitiesOfType<GUI>();
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