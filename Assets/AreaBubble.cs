using System.Collections;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class AreaBubble : BubbleBase
{
    private const float DELAY_BETWEEN_POPS = .06f;

    [HideInInspector]
    public AreaBubbleConfig config;

    [FormerlySerializedAs("jsonPath")] public string configName;

    protected override void Start()
    {
        base.Start();
        TextAsset file = Resources.Load<TextAsset>("BubbleConfigs/" + configName);
        config = JsonUtility.FromJson<AreaBubbleConfig>(file.text);
    }

    public override void Pop()
    {
        base.Pop();
        StartCoroutine(PopNeighbours());
    }

    IEnumerator PopNeighbours()
    {
        foreach (var area in config.areas)
        {
            var percentile = Random.Range(0f, 1f);

            if (percentile < area.percentage)
            {
                foreach (var bubbleObject in BubbleUtils.GetBubblesInArea(gridPosition, area.Area))
                {
                    yield return new WaitForSeconds(DELAY_BETWEEN_POPS);

                    bubbleObject.GetComponent<BubbleBase>().Pop();
                }
            }
        }
    }
}