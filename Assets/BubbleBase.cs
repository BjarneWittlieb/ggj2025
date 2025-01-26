using UnityEngine;

public class BubbleBase : MonoBehaviour
{
    [SerializeField]  public int        score = 1;
    [SerializeField]  public float      scoreFactor;
    [SerializeField]  public string     name;
    [HideInInspector] public Vector2Int gridPosition;
    
    public GameObject particleEffectPrefab;
    
    // FIELDS
    private bool       _isPopped;
    
    // DEPENDENCIES
    private ScoreLogic _scoreLogic;
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _scoreLogic = GameObject.Find("ScoreLogic").GetComponent<ScoreLogic>();
        particleEffectPrefab = Resources.Load<GameObject>("Prefabs/Airpop");
        
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Pop()
    {
        if (_isPopped)
            return;
        
        _isPopped = true;
        
        // Sound
        PlayPopSound();
        
        // ParticleEffect
        GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        
        // Score
        _scoreLogic.AddToScore(score);

        // replace sprite with random popped-sprite
        SpriteRenderer renderer = (SpriteRenderer)gameObject.GetComponentInChildren(typeof(SpriteRenderer));
        int randomNumber = Random.Range(1, 4);
        renderer.sprite = Resources.Load<Sprite>("Sprites/bubbles-popped" + randomNumber);
    }

    private void PlayPopSound()
    {
        if (_audioSource)
        {
            _audioSource.Play();
        }   
    }
}