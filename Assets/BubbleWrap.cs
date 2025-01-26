using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
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
        tilemap             = _grid.GetComponentInChildren<Tilemap>();
        _loadedBubblePrefab = Resources.Load<GameObject>("Prefabs/BubbleTypes/BasicBubble");

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
}