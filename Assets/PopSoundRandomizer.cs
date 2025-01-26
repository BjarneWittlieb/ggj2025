using UnityEngine;

public class PopSoundRandomizer : MonoBehaviour
{
    private string[] popSoundPaths = new[]
    {
        "Sounds/popping-bubble1.wav",
        "Sounds/popping-bubble2.wav",
        "Sounds/popping-bubble3.wav",
        "Sounds/popping-bubble4.wav",
        "Sounds/popping-bubble5.wav",
        "Sounds/popping-bubble6.wav"
    };
    
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        var randomPath = popSoundPaths[Random.Range(0, popSoundPaths.Length)];
        Debug.Log("sound path:");
        Debug.Log(randomPath);
        Debug.Log(Resources.Load<AudioClip>(randomPath));
        _audioSource.resource = Resources.Load<AudioClip>(randomPath);
    }
}
