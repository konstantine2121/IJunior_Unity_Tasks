using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImperialMarchPlayer : MonoBehaviour
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
        Debug.Log("enter");
        _audioSource.Play();
        StartCoroutine(IncreaseMusicVolume());
    }

    public void Stop()
    {
        Debug.Log("exit");
        StartCoroutine(DecreaseMusicVolume());
    }

    private IEnumerator IncreaseMusicVolume()
    {
        while (_audioSource.volume < MaxVolume)
        {
            var nextValue = _audioSource.volume + _increaseRate;
            if (nextValue > MaxVolume)
            {
                nextValue = MaxVolume;
            }

            _audioSource.volume = nextValue;

            yield return new WaitForSeconds(CoroutineDelay);
        }
    }

    private IEnumerator DecreaseMusicVolume()
    {
        while (_audioSource.volume > MinVolume)
        {
            var nextValue = _audioSource.volume - _increaseRate;
            if (nextValue < MinVolume)
            {
                nextValue = MinVolume;
            }

            _audioSource.volume = nextValue;

            if (nextValue == MinVolume)
            {
                _audioSource.Stop();
            }

            yield return new WaitForSeconds(CoroutineDelay);
        }
    }

}
