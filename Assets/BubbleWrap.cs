using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using Utils;

public class BubbleWrap : MonoBehaviour
{
    public Vector2Int upperLeft;

    public Vector2Int lowerRight;

    private Grid _grid;
    
    private List<GameObject> _bubblePrefabs = new List<GameObject>();

    private readonly Dictionary<Vector2Int, GameObject> _allBubbles = new ();
    private          Tilemap                            tilemap;

    private float _poppingCooldown;
    
    // OUTPUTS
    public event Action PoppingStarted;
    public event Action PoppingEnded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grid               = GetComponent<Grid>();
        tilemap             = _grid.GetComponentInChildren<Tilemap>();
        
        _bubblePrefabs
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/LineAreaBubble"), 1)
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/ScatterAreaBubble"), 1)
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/Diamond-AreaBubble"), 1)
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/MinusBubble"), 1)
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/GoldenBubble"), 1)
            .AddMultiple(Resources.Load<GameObject>("Prefabs/BubbleTypes/BasicBubble"), 100)
            ;
        
        // -1 means that popping has not started
        _poppingCooldown = -1f;
        
        StartCoroutine(SpawnBubbles());
    }

    void Update()
    {
        if (_poppingCooldown > 0)
        {
            _poppingCooldown -= Time.deltaTime;
            if (_poppingCooldown <= 0)
            {
                PoppingEnded?.Invoke();
            }
        } 
    }
    
    IEnumerator SpawnBubbles()
    {
        for (int x = upperLeft.x; x <= lowerRight.x; x++)
        {
            for (int y = upperLeft.y; y >= lowerRight.y; y--)
            {
                var index = UnityEngine.Random.Range(0, _bubblePrefabs.Count);
                
                GameObject loadedBubble =
                    Instantiate(_bubblePrefabs[index], new Vector3(x, y, 0), Quaternion.identity, transform);

                PlaceBubble(loadedBubble, new Vector2Int(x, y));
            }
        }

        yield return null;
    }

    public void PlaceBubble(GameObject loadedBubble, Vector2Int gridPosition)
    {
        loadedBubble.transform.SetParent(transform, true);
        ReplaceBubble(loadedBubble, gridPosition);

        var bubble = loadedBubble.GetComponent<BubbleBase>();
        bubble.gridPosition = new Vector2Int(gridPosition.x, gridPosition.y);
        loadedBubble.transform.position = tilemap.CellToWorld(new Vector3Int(gridPosition.x, gridPosition.y));

        Debug.Log("parent name:" + loadedBubble.transform.parent.name);
    }

    private void ReplaceBubble(GameObject loadedBubble, Vector2Int gridPosition)
    {
        Destroy(GetBubble(gridPosition));
        _allBubbles[gridPosition] = loadedBubble;
    }

    public GameObject GetBubble(Vector2Int gridPos) => _allBubbles.GetValueOrDefault(gridPos);

    public GameObject GetBubble(Vector3 worldPos)
    {
        var gridPos = tilemap.WorldToCell(worldPos);
        return GetBubble(new Vector2Int(gridPos.x, gridPos.y));
    }

    public void UpdateBubblePop()
    {
        // on first pop, cool down is less 0
        if (_poppingCooldown < 0)
        {
            PoppingStarted?.Invoke();
        }
        _poppingCooldown = 0.2f;
    }
}