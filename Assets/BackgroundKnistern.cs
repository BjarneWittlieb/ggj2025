using System;
using UnityEngine;

public class BackgroundKnistern : MonoBehaviour
{
    private BubbleWrap _bubbleWrap;
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _bubbleWrap = GetComponent<BubbleWrap>();

        _bubbleWrap.PoppingStarted += StartSound;
        _bubbleWrap.PoppingEnded += StopSound;
    }

    private void StartSound()
    {
        _audioSource.Play();
    }

    private void StopSound()
    {
        _audioSource.Stop();
    }

    private void OnDestroy()
    {
        _bubbleWrap.PoppingStarted -= StartSound;
        _bubbleWrap.PoppingEnded -= StopSound;
    }
}
