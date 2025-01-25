using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BubbleWrap : MonoBehaviour
{
    public Vector2Int upperLeft;
    
    public Vector2Int lowerRight;

    private Grid _grid;

    private GameObject _loadedBubblePrefab;
    
    private readonly Dictionary<Vector2Int, GameObject> _allBubbles = new ();
    private          Tilemap                            tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grid               = GetComponent<Grid>();
        tilemap             = _grid.GetComponent<Tilemap>();
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
                
                PlaceBubble(loadedBubble, new Vector3Int(x, y));
                _allBubbles[new Vector2Int(x, y)] = loadedBubble;
            }
        }
        yield return null;
    }

    private void PlaceBubble(GameObject loadedBubble, Vector3Int gridPosition)
    {
        var bubble = loadedBubble.GetComponent<BubbleBase>();
        bubble.gridPosition = new Vector2Int(gridPosition.x, gridPosition.y);
        loadedBubble.transform.position = tilemap.CellToWorld(gridPosition);
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
