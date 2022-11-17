using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImperialMarchPlayer : MonoBehaviour
{
    private const float MinVolume = 0;
    private const float MaxVolume = 1;

    [SerializeField] [Range(0.0f, 1.0f)] private float _defaultVolume = 0.5f;
    [SerializeField] [Range(0.0f, 100f)] private float _maxDistance =10.0f;
    [SerializeField] private Transform _dartWeider;
    private AudioSource _audioSource;

    private bool UseDefaultVolume => _dartWeider == null;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        _audioSource.volume = GetMusicVolume();
        //Debug.Log("_audioSource.volume " +_audioSource.volume);        
    }

    private float GetMusicVolume()
    {
        if (UseDefaultVolume)
        {
            return _defaultVolume;
        }

        var volume = Mathf.MoveTowards(MinVolume, MaxVolume, GetMarchPower());
        
        //Debug.Log("volume " + volume);        

        return volume;
    }

    private float GetMarchPower()
    {
        var distance = GetDistanceToDartWeider();
        //Debug.Log("distance " + distance);

        if (distance > _maxDistance || _maxDistance == 0)
        {
            return MinVolume;
        }

        float power = _defaultVolume;

        try
        {
            power = MaxVolume - distance / _maxDistance;
        }
        catch { }
        //Debug.Log("power " + power);
        return power;
    }

    private float GetDistanceToDartWeider()
    {
        if (_dartWeider == null)
        {
            return 0;
        }

        return Vector3.Distance(_dartWeider.position, transform.position);
    }
}
