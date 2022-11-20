using System.Collections;
using UnityEngine;

namespace CatBurglar_Task
{

    [RequireComponent(typeof(AudioSource))]
    public class AlarmPlayer : MonoBehaviour
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
            StartCoroutine(ChangeMusicVolume(true));
        }

        public void Stop()
        {
            StartCoroutine(ChangeMusicVolume(false));
        }

        private IEnumerator ChangeMusicVolume(bool increase)
        {
            var delay = new WaitForSeconds(CoroutineDelay);

            do
            {
                _audioSource.volume = GetNextVolumeValue(increase);

                yield return delay;
            }
            while (_audioSource.volume < MaxVolume &&
                _audioSource.volume > MinVolume);

            if (_audioSource.volume == MinVolume)
            {
                _audioSource.Stop();
            }

        }

        private float GetNextVolumeValue(bool increase)
        {
            var nextValue = increase ?
                _audioSource.volume + _increaseRate :
                _audioSource.volume - _increaseRate;

            if (nextValue > MaxVolume)
            {
                nextValue = MaxVolume;
            }

            if (nextValue < MinVolume)
            {
                nextValue = MinVolume;
            }

            return nextValue;
        }

    }
}