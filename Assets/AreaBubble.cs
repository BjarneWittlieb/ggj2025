using System.Collections;
using Models;
using UnityEngine;
using Utils;

public class AreaBubble : BubbleBase
{
    public AreaBubbleType bubbleType;
        
    public override void Pop()
    {
        base.Pop();

        StartCoroutine(PopNeighbours());
    }
    
    IEnumerator PopNeighbours()
    {
        var delay = Random.Range(0.1f, 0.2f);
            
        foreach (var area in bubbleType.areas)
        {
            var percentile = Random.Range(0f, 1f);

            if (percentile < area.percentage)
            {
                foreach (var bubbleObject in BubbleUtils.GetBubblesInArea(gridPosition, area.Area))
                {
                    yield return new WaitForSeconds(delay);
                        
                    bubbleObject.GetComponent<BubbleBase>().Pop();
                }
            }
        }
    }
}