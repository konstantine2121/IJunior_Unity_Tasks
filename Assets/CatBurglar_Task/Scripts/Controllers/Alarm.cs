using System.Collections;
using UnityEngine;

namespace CatBurglar_Task
{

    [RequireComponent(typeof(AudioSource))]
    public class Alarm : MonoBehaviour
    {
        public const float MinVolume = 0;
        public const float MaxVolume = 1;
        private const float CoroutineDelay = 0.1f;

        [SerializeField] [Range(0.0f, 1.0f)] public float _increaseRate = 0.02f;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _audioSource.volume = MinVolume;
        }

        public void Play()
        {
            _audioSource.Play();
            StartCoroutine(ChangeMusicVolume(MaxVolume));
        }

        public void Stop()
        {
            StartCoroutine(ChangeMusicVolume(MinVolume));
        }

        private IEnumerator ChangeMusicVolume(float targetVolume)
        {
            var delay = new WaitForSeconds(CoroutineDelay);
            
            do
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _increaseRate);

                yield return delay;
            }
            while (_audioSource.volume != targetVolume);

            if (_audioSource.volume == MinVolume)
            {
                _audioSource.Stop();
            }
        }
    }
}