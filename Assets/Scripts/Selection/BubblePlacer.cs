using UnityEngine;

namespace Selection
{
    public class BubblePlacer: MonoBehaviour
    {
        private BubbleSelector _bubbleSelector;
        
        void Start()
        {
            _bubbleSelector = GetComponent<BubbleSelector>();
        }
    }
}