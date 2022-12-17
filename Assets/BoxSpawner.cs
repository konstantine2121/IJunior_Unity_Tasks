using SpawnEnemies_Task;
using System.Collections;
using UnityEngine;

namespace Platformer2D_Task
{
    public class BoxSpawner : MonoBehaviour
    {
        private const float Interval = 5;

        [SerializeField] private Box _box;

        private int _currentIndex = 0;
        private SpawnZone[] _spawns = new SpawnZone[0];
        private WaitForSeconds _delay = new WaitForSeconds(Interval);

        private bool CanSpawn
        {
            get
            {
                return _box != null &&
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

            while (this.enabled)
            {
                yield return _delay;
                SpawnBox();
            }
        }

        private void SpawnBox()
        {
            if (!CanSpawn)
            {
                return;
            }

            var position = _spawns[_currentIndex].transform.position;

            Instantiate(_box, position, Quaternion.identity);
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