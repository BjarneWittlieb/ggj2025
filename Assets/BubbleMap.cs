using System.Collections.Generic;
using UnityEngine;

public class BubbleMap : MonoBehaviour
{
    private readonly Dictionary<Vector2Int, BubbleBase> bubbles = new();
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Vector2Int pos, GameObject bubble)
    {
        var bubbleBase = bubble.GetComponent<BubbleBase>();
        bubbleBase.mapPosition = pos;
        bubbles.Add(pos, bubbleBase);
    }

    public bool TryGetBubble(Vector2Int pos, out BubbleBase bubbleBase)
    {
        return bubbles.TryGetValue(pos, out bubbleBase);
    }

}
