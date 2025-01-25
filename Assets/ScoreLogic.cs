using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ScoreLogic : MonoBehaviour
{
    private int _currentScore = 0;
    public TMP_Text scoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddToScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        scoreText.text = _currentScore.ToString();
    }
    
}
