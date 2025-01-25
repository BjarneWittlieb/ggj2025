using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLogic : MonoBehaviour
{
    private int _currentScore = 0;
    public TMP_Text scoreText;
    public int scoreForStar1;
    public int scoreForStar2;
    public int scoreForStar3;
    public Image star1;
    public Image star2;
    public Image star3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentScore >= scoreForStar3 && !star3.enabled)
        {
            star1.enabled = true; 
            star2.enabled = true; 
            star3.enabled = true; 
        }
        else if (_currentScore >= scoreForStar2 && !star2.enabled)
        {
            star2.enabled = true; 
            star1.enabled = true; 
        }        
        else if (_currentScore >= scoreForStar1 && !star1.enabled)
        {
            star1.enabled = true; 
        }
    }

    public void AddToScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        scoreText.text = _currentScore.ToString();
    }
    
}
