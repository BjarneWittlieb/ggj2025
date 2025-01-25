using UnityEngine;
using UnityEngine.Analytics;

public class BubbleSpriteRandomizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var enabledIndex = Random.Range(0, spriteRenderers.Length);

        for (var i = 0; i < spriteRenderers.Length; i++)
        {
            if (i == enabledIndex)
            {
                continue;
            }
            
            Destroy(spriteRenderers[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
