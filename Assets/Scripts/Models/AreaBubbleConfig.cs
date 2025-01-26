using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class AreaBubbleConfig: BubbleType
    {
        [SerializeField] public BubbleAreaWithPercentage[] areas;

        public AreaBubbleConfig(BubbleAreaWithPercentage[] areas)
        {
            this.areas = areas;
        }
    }
}