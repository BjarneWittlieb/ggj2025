using System.Linq;
using System.Xml.Linq;
using Models;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    public Vector2Int[] pattern;

    public AreaBubbleType GetPattern()
    {
        AreaBubbleType areaBubbleType = new AreaBubbleType(new[]
        {
            new BubbleAreaWithPercentage(pattern.Select(vec => new Vector2IntData(vec.x, vec.y)).ToList(), 1f)
        });
        
        Debug.Log("GENERATED PATTERN:");
        Debug.Log(JsonUtility.ToJson(areaBubbleType));
        
        return areaBubbleType;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
