using UnityEngine;

public class BubbleBase : MonoBehaviour
{
    [SerializeField]  public int        score = 1;
    [SerializeField]  public float      scoreFactor;
    [SerializeField]  public string     name;
    [HideInInspector] public Vector2Int gridPosition;
    
    public GameObject particleEffectPrefab;
    
    private bool       isPopped;
    private ScoreLogic scoreLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        scoreLogic = GameObject.Find("ScoreLogic").GetComponent<ScoreLogic>();
        particleEffectPrefab = Resources.Load<GameObject>("Prefabs/Airpop");
    }

    public virtual void Pop()
    {
        if (isPopped)
            return;
        
        isPopped = true;
        
        // ParticleEffect
        GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        
        // Score
        scoreLogic.AddToScore(score);

        // replace sprite with random popped-sprite
        SpriteRenderer renderer = (SpriteRenderer)gameObject.GetComponentInChildren(typeof(SpriteRenderer));
        int randomNumber = Random.Range(1, 4);
        renderer.sprite = Resources.Load<Sprite>("Sprites/bubbles-popped" + randomNumber);
    }
}