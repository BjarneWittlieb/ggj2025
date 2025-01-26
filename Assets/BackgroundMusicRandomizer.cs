using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicRandomizer : MonoBehaviour
{
    public int secondsBetweenSongs = 2;

    private AudioClip[] _soundtracks;
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundtracks = Resources.LoadAll<AudioClip>("Sounds/Levelmusic");
        
        StartCoroutine(WaitAndPlay());
    }

    private IEnumerator WaitAndPlay()
    {
        float seconds = Random.Range(secondsBetweenSongs - 1f, secondsBetweenSongs + 1f);
        
        Debug.Log("wait " + seconds);
        yield return new WaitForSeconds(seconds);
        
        StartCoroutine(PlaySoundAndWait());
    }
    
    // Or using Coroutine
    private IEnumerator PlaySoundAndWait()
    {
        Debug.Log("playing sound");
        _audioSource.resource = _soundtracks[Random.Range(0, _soundtracks.Length)];
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        WaitAndPlay();
    }
}
