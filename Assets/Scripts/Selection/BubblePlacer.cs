using System;
using System.Data;
using DefaultNamespace;
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

        void HandleSelect(GameObject bubble, BubbleType selectedType)
        {
            // The bubble is now the new bubble type, So delete previous bubble type
            BubbleBase bubbleBase = bubble.GetComponent<BubbleBase>();
            Destroy(bubbleBase);
            
            switch (selectedType)
            {
                case AreaBubbleType areaBubbleType:
                    var areaBubble = gameObject.AddComponent<AreaBubble>();
                    areaBubble.bubbleType = areaBubbleType;
                    break;
                default:
                    throw new NotImplementedException("Currently only area bubble is placeable.");
            }
        }
    }
}