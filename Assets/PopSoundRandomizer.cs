using UnityEngine;

public class PopSoundRandomizer : MonoBehaviour
{
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        var popSounds = Resources.LoadAll<AudioClip>("Sounds/Defaultpop");
        _audioSource.resource = popSounds[Random.Range(0, popSounds.Length)];
    }
}
