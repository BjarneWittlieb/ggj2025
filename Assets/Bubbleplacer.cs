using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bubbleplacer : MonoBehaviour
{
    public Grid grid;
    
    private Tilemap tilemap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = grid.GetComponentInChildren<Tilemap>();
    }

    public void placeOnGridPosition(int x, int y)
    {
        if (tilemap == null)
        {
            tilemap = grid.GetComponentInChildren<Tilemap>();
        }
        transform.position = tilemap.CellToWorld(new Vector3Int(x, y, 0));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
