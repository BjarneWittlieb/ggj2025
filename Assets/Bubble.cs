using UnityEngine;

public class Bubble : MonoBehaviour
{
    private BubbleMap bubbleMap;

    [SerializeField]
    public Vector2Int[] pattern;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleMap = GetComponent<BubbleMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pop()
    {
        foreach (var pos in pattern)
        {
            if (bubbleMap.TryGetBubble(pos, out var bubble))
                bubble.GetComponent<Bubble>().Pop();
        }
        
        Destroy(gameObject);
    }
}
