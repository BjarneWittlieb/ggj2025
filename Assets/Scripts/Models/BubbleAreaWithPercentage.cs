using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class BubbleAreaWithPercentage
    {
        [SerializeField] public List<Vector2IntData> area;

        public List<Vector2Int> Area => area.Select(data => new Vector2Int(data.x, data.y)).ToList();

        [SerializeField] public float percentage;
        
        public BubbleAreaWithPercentage(List<Vector2IntData> area, float percentage)
        {
            this.area = area;
            this.percentage = percentage;
        }
    }
}