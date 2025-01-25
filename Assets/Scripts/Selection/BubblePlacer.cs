using System;
using Models;
using UnityEngine;

namespace Selection
{
    public class BubblePlacer: MonoBehaviour
    {
        private BubbleSelector _bubbleSelector;
        
        void Start()
        {
            _bubbleSelector = GetComponent<BubbleSelector>();

            _bubbleSelector.OnSelect += HandleSelect;
        }

        void OnDestroy()
        {
            _bubbleSelector.OnSelect -= HandleSelect;
        }

        void HandleSelect(GameObject original, GameObject replacement)
        {
            Instantiate(replacement, original.transform.position, Quaternion.identity, original.transform.parent);
            // TODO set in bubblewrap
            
            // The bubble is now the new bubble type, So delete previous bubble type
            Destroy(original);
            
            /*
            switch (selectedType)
            {
                case AreaBubbleType areaBubbleType:
                    var areaBubble = gameObject.AddComponent<AreaBubble>();
                    areaBubble.bubbleType = areaBubbleType;
                    break;
                default:
                    throw new NotImplementedException("Currently only area bubble is placeable.");
            }
            */
        }
    }
}