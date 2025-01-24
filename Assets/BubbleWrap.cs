using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BubbleWrap : MonoBehaviour
{
    public Vector2Int upperLeft;
    
    public Vector2Int lowerRight;

    private Grid _grid;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grid = GetComponent<Grid>();
        
        GameObject loadedBubblePrefab = Resources.Load<GameObject>("Prefabs/BasicBubble");

        for (int x = upperLeft.x; x <= lowerRight.x; x++)
        {
            for (int y = upperLeft.y; y >= lowerRight.y; y--)
            {
                GameObject loadedBubble = Instantiate(loadedBubblePrefab, new Vector3(x, y, 0), Quaternion.identity);
                Bubbleplacer placer = loadedBubble.GetComponent<Bubbleplacer>();
                placer.grid = _grid;
                placer.placeOnGridPosition(x, y);
            }
        }
    }

    
    IEnumerator<WaitForEndOfFrame> InstantiateAndInitialize(int x, int y, GameObject prefab)
    {
        GameObject loadedBubble = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        yield return new WaitForEndOfFrame();
        Bubbleplacer placer = loadedBubble.GetComponent<Bubbleplacer>();
        placer.grid = _grid;
        placer.placeOnGridPosition(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
