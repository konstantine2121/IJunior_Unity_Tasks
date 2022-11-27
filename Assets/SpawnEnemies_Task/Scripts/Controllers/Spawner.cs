using System.Collections;
using UnityEngine;

namespace SpawnEnemies_Task
{
    public class Spawner : MonoBehaviour
    {
        private const float Interval = 2;
        private const float VerticalOffset = -0.5f;

        [SerializeField] private Trooper _trooper;

        private int _currentIndex = 0;
        private SpawnZone[] _spawns = new SpawnZone[0];
        private WaitForSeconds _delay = new WaitForSeconds(Interval);

        private bool CanSpawn
        { 
            get 
            { 
                return _trooper != null && 
                    _spawns != null && 
                    _spawns.Length != 0; 
            } 
        }

        private void Awake()
        {
            _spawns = GetComponentsInChildren<SpawnZone>();
        }

        private void OnEnable()
        {
            StartCoroutine(StartSpawnLoop());
        }

        private IEnumerator StartSpawnLoop()
        {
            if (!CanSpawn)
            {
                yield break;
            }

            while(this.enabled)
            {
                yield return _delay;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            if (!CanSpawn)
            {
               return;
            }

            var position = _spawns[_currentIndex].transform.position;
            position.y += VerticalOffset;

            Instantiate(_trooper, position, Quaternion.identity);
            IncrementIndex();
        }

        private void IncrementIndex()
        {
            _currentIndex++;

            if (_currentIndex >= _spawns.Length)
            {
                _currentIndex = 0;
            }
        }
    }
    
}