using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class BubbleBase : MonoBehaviour
{
    private bool _isPopped = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public virtual void Pop()
    {
        if (!_isPopped)
        {
            _isPopped = true;
        }
        
        // TODO start pop animation and sound and such
        Destroy(gameObject);
    }
}
