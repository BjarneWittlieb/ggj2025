using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class BubbleBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public virtual void Pop()
    {
        // TODO start pop animation and sound and such
        Destroy(gameObject);
    }
}
