using UnityEngine;

public class Bubbleplacer : MonoBehaviour
{
    public Grid grid;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeAt(1, 5);
        
    }

    void placeAt(int x, int y)
    {
        Vector3 cellSize = grid.cellSize;
        
        float positionX = x * cellSize.x + grid.transform.position.x;
        float positionY = y * cellSize.y + grid.transform.position.y;
        
        transform.position = new Vector3(positionX, positionY, transform.position.z);
        
        Debug.Log(transform.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
