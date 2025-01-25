using UnityEngine;
using UnityEngine.Analytics;

public class BubbleSpriteRandomizer : MonoBehaviour
{
    [SerializeField]
    public Sprite[] sprites;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.sprite = sprite;
    }
}
