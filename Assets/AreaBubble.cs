using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class AreaBubble : BubbleBase
{
    private const float DELAY_BETWEEN_POPS = .06f;

    [HideInInspector]
    public AreaBubbleConfig config;
    public GameObject linePrefab;
    private GameObject instantiatedLine;
    private List<GameObject> lines = new List<GameObject>();
    
    [FormerlySerializedAs("jsonPath")] public string configName;

    protected override void Start()
    {
        base.Start();
        TextAsset file = Resources.Load<TextAsset>("BubbleConfigs/" + configName);
        config = JsonUtility.FromJson<AreaBubbleConfig>(file.text);
        linePrefab = Resources.Load<GameObject>("Prefabs/TargetLines");
        instantiatedLine = Instantiate(linePrefab);
        lines.Add(instantiatedLine);

    }

    public void Update()
    {
        if (this.transform.IsChildOf(GameObject.Find("grid").transform))
        {
            foreach (var area in config.areas)
            {
                foreach (var bubbleObject in BubbleUtils.GetBubblesInArea(gridPosition, area.Area))
                {
                    LineRenderer lineRenderer = instantiatedLine.GetComponentInChildren<LineRenderer>();
                    if (lineRenderer is not null)
                    {
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, this.transform.position); // Startpunkt
                        lineRenderer.SetPosition(1, bubbleObject.transform.position); // Endpunkt
                    }
                }
            }
        }
    }
    
    public override void Pop()
    {
        var allLines = GameObject.FindObjectsByType<LineRenderer>(FindObjectsSortMode.None);

        foreach (var line in allLines)
        {
            Destroy(line);
        }
        
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

    public void OnDestroy()
    {
        foreach (var line in lines)
        {
            Destroy(line);
        }
    }
}