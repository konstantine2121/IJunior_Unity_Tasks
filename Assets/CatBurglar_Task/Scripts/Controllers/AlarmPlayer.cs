using System.Collections;
using UnityEngine;

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
        StartCoroutine(IncreaseMusicVolume());
    }

    public void Stop()
    {
        StartCoroutine(DecreaseMusicVolume());
    }

    private IEnumerator IncreaseMusicVolume()
    {
        while (_audioSource.volume < MaxVolume)
        {
            _audioSource.volume = GetNextVolumeValue(true);

            yield return new WaitForSeconds(CoroutineDelay);
        }
    }

    private IEnumerator DecreaseMusicVolume()
    {
        while (_audioSource.volume > MinVolume)
        {            
            _audioSource.volume = GetNextVolumeValue(false);

            if (_audioSource.volume == MinVolume)
            {
                _audioSource.Stop();
            }

            yield return new WaitForSeconds(CoroutineDelay);
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
