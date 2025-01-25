using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BubbleWrap : MonoBehaviour
{
    public Vector2Int upperLeft;
    
    public Vector2Int lowerRight;

    private Grid _grid;

    private GameObject _loadedBubblePrefab;
    
    private readonly Dictionary<Vector2Int, GameObject> _allBubbles = new ();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grid = GetComponent<Grid>();
        _loadedBubblePrefab = Resources.Load<GameObject>("Prefabs/BasicBubble");

        StartCoroutine(SpawnBubbles());
    }
    
    IEnumerator SpawnBubbles()
    {
        for (int x = upperLeft.x; x <= lowerRight.x; x++)
        {
            for (int y = upperLeft.y; y >= lowerRight.y; y--)
            {
                GameObject loadedBubble =
                    Instantiate(_loadedBubblePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                Bubbleplacer placer = loadedBubble.GetComponent<Bubbleplacer>();

                placer.grid = _grid;

                placer.placeOnGridPosition(x, y);
                _allBubbles[new Vector2Int(x, y)] = loadedBubble;
            }
        }
        yield return null;
    }
    
    public GameObject GetBubble(Vector2Int pos)
    {
        if (_allBubbles.ContainsKey(pos)) 
        {
            return _allBubbles[pos];
        }
        return null;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
