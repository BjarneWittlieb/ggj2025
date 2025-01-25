using UnityEngine;

public class BubbleWobble : MonoBehaviour
{
    public  float wobbleSpeed = 2f;   
    public  float wobbleScale = 0.02f; 

    private Vector3 initialScale;
    private float   randomOffsetX;       
    private float   randomOffsetY;  

    void Start()
    {
        initialScale  =  transform.localScale;
        wobbleSpeed   += Random.Range(-.2f, .2f);
        wobbleScale   += Random.Range(wobbleScale / 2f, wobbleScale / 2f);
        randomOffsetX =  Random.Range(0f, Mathf.PI * 2f);
        randomOffsetY =  Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed + randomOffsetX) * wobbleScale;
        float wobbleY = Mathf.Cos(Time.time * wobbleSpeed + randomOffsetY) * wobbleScale;

        // wobble to scale
        transform.localScale = new Vector3(
                                           initialScale.x + wobbleX,
                                           initialScale.y + wobbleY,
                                           initialScale.z
                                          );

        // rotate???
        // transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wobbleSpeed + randomOffsetX) * wobbleScale * 20f);
    }
}
