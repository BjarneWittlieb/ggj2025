using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleBase : MonoBehaviour
{
    private BubbleMap bubbleMap;

    [SerializeField]
    public Vector2Int[] pattern;
    
    [FormerlySerializedAs("gridPosition")] [HideInInspector]
    public Vector2Int mapPosition;

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
        StartCoroutine(PopNeighbours());
        Destroy(gameObject);
    }

    IEnumerator PopNeighbours()
    {
        var delay = 0f;
        foreach (var offset in pattern.OrderBy(_ => Random.value))
        {
            delay += Random.Range(0.1f, 0.2f);
            if (bubbleMap.TryGetBubble(mapPosition + offset, out var bubble))
                bubble.Pop();
            
            yield return new WaitForSeconds(delay);
        }
    }
}
