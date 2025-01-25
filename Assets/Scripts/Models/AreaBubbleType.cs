using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class AreaBubbleType: BubbleType
    {
        [SerializeField] public BubbleAreaWithPercentage[] areas;

        public AreaBubbleType(BubbleAreaWithPercentage[] areas)
        {
            this.areas = areas;
        }
    }
}