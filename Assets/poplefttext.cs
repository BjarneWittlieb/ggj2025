using Selection;
using TMPro;
using UnityEngine;

public class poplefttext : MonoBehaviour
{
    CurrentLevelContext _currentLevelContext;
    TMP_Text _text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLevelContext = GameObject.Find("grid").GetComponent<CurrentLevelContext>();
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"{_currentLevelContext.PopsLeft} pops left";
    }
}
