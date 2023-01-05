using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(EntitiesFactory))]
    public class SceneLoader : MonoBehaviour, IGameController
    {
        [SerializeField] private EntitiesFactory _entytiesFactory;        
        [SerializeField] private Transform _patrolWaypointsContainer;
        [SerializeField] private Vector3 _playerSpawnPosition;
        [SerializeField] private Vector3 _gundamSpawnPosition;

        private Player _player;
        private GUI _gui;

        private WaitForSeconds _deathActionTimeout = new WaitForSeconds(2);

        private void Start()
        {
            _gui = _entytiesFactory.CreateGUI();
            _gui.RegisterController(this);
        }

        public void Restart()
        {
            Stop();            
            SpawnEntities();
            Resume();
        }

        public void Stop()
        {
            RemoveEntities();
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
            SpawnPlayer();
            _entytiesFactory.CreateGundam(_gundamSpawnPosition, GetGundamPatrolPoints());
            _entytiesFactory.CreateBoxSpawner();
        }

        private void SpawnPlayer()
        {
            if (_player != null)
            {
                _player.Health.Died -= OnPlayerDied;
            }

            _player = _entytiesFactory.CreatePlayer(_playerSpawnPosition);
            _gui.RegisterPlayer(_player);
            _player.Health.Died += OnPlayerDied;
        }

        private void OnPlayerDied(Health health, float hp)
        {
            StartCoroutine(PerformAfterDeath());
        }

        private System.Collections.IEnumerator PerformAfterDeath()
        {
            yield return _deathActionTimeout;

            Pause();
            _gui.MenuContainer.ShowScreen(MenuType.GameOver);
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