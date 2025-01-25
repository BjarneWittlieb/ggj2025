using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class BubbleBase : MonoBehaviour
{
    private bool _isPopped = false;
    private int _value = 1;
    private ScoreLogic scoreLogic;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        scoreLogic = GameObject.Find("ScoreLogic").GetComponent<ScoreLogic>();
    }
    
    public virtual void Pop()
    {
        if (!_isPopped)
        {
            _isPopped = true;
        }
        
        scoreLogic.AddToScore(_value);
        
        // TODO start pop animation and sound and such
        Destroy(gameObject);
    }
}
