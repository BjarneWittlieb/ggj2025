using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bubbleplacer : MonoBehaviour
{
    public Grid grid;
    
    private Tilemap tilemap;

    public Vector2Int CurrentPosition { get; private set; }

    public void placeOnGridPosition(int x, int y)
    {
        tilemap = grid.GetComponentInChildren<Tilemap>();
        transform.position = tilemap.CellToWorld(new Vector3Int(x, y, 0));
        
        CurrentPosition = new Vector2Int(x, y);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
