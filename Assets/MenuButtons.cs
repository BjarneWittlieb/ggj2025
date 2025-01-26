using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Methode zum Laden einer Szene
    public void LoadScene(string sceneName)
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
            Debug.Log("Playing sound lol");
        }
        
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    // Optional: Methode, um die Anwendung zu beenden
    public void QuitApplication()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
            Debug.Log("Playing sound lol");
        }

        
        Debug.Log("Quitting application");
        Application.Quit();
    }

}
