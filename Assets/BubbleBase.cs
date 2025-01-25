using UnityEngine;

public class BubbleBase : MonoBehaviour
{
    [SerializeField]  public int        score = 1;
    [SerializeField]  public float      scoreFactor;
    [SerializeField]  public string     name;
    [HideInInspector] public Vector2Int gridPosition;
    
    private bool       isPopped;
    private ScoreLogic scoreLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        scoreLogic = GameObject.Find("ScoreLogic").GetComponent<ScoreLogic>();
    }

    public virtual void Pop()
    {
        if (!isPopped)
            isPopped = true;

        scoreLogic.AddToScore(score);

        // TODO start pop animation and sound and such
        Destroy(gameObject);
    }
}