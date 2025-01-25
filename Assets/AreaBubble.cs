using System.Collections;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class AreaBubble : BubbleBase
{
    public AreaBubbleType bubbleType;

    [FormerlySerializedAs("jsonPath")] public string configName;

    public void Start()
    {
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("BubbleConfigs/" + configName + ".json");
        foreach (TextAsset file in jsonFiles)
        {
            bubbleType = JsonUtility.FromJson<AreaBubbleType>(file.text);
        }
    }
    
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