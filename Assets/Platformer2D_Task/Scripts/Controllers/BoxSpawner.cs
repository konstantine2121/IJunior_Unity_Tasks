using SpawnEnemies_Task;
using System.Collections;
using UnityEngine;

namespace Platformer2D_Task
{
    public class BoxSpawner : MonoBehaviour
    {
        private const string BoxPrefab = "GearBox";
        private const string MedicalKitPrefab = "MedicalKit";
        private const float Interval = 4;

        [SerializeField][Range(0,100)]private int _medicalKitSpawnChance = 20;

        private Box _box;
        private MedicalKit _medKit;

        private int _currentIndex = 0;
        private SpawnZone[] _spawns = new SpawnZone[0];
        private WaitForSeconds _delay = new WaitForSeconds(Interval);

        private bool CanSpawn
        {
            get
            {
                return _box != null &&
                     _medKit != null &&
                    _spawns != null &&
                    _spawns.Length != 0;
            }
        }

        private void Awake()
        {
            GetLinks();
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

            Instantiate(GetPrefab(), position, Quaternion.identity);
            IncrementIndex();
        }

        private MonoBehaviour GetPrefab()
        {
            return CheckMedkitChance() ?
                _medKit :
                _box;
        }

        private bool CheckMedkitChance()
        {
            const int percentage = 100;
            var number = Random.Range(0, percentage);

            return _medicalKitSpawnChance > number;
        }

        private void IncrementIndex()
        {
            _currentIndex++;

            if (_currentIndex >= _spawns.Length)
            {
                _currentIndex = 0;
            }
        }

        private void GetLinks()
        {
            _box = Resources.Load<Box>(BoxPrefab);
            _medKit = Resources.Load<MedicalKit>(MedicalKitPrefab);
        }
    }
}