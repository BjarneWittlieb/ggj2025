using UnityEngine;

namespace Models
{
    public class BubbleAreaWithPercentage
    {
        [SerializeField] private Vector2Int[] area;

        public Vector2Int[] Area => area;

        [SerializeField] private float percenetage;
        
        public float Percentage => percenetage;
    }
}