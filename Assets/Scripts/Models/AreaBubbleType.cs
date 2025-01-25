using UnityEngine;

namespace Models
{
    public class AreaBubbleType: BubbleType
    {
        [SerializeField] private BubbleAreaWithPercentage[] areas;

        public BubbleAreaWithPercentage[] Areas => areas;
    }
}