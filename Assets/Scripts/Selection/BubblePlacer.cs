using System;
using Models;
using UnityEngine;

namespace Selection
{
    public class BubblePlacer: MonoBehaviour
    {
        private BubbleSelector _bubbleSelector;
        private BubbleWrap     _bubbleWrap;

        void Start()
        {
            _bubbleWrap = FindFirstObjectByType<BubbleWrap>();
            _bubbleSelector = GetComponent<BubbleSelector>();

            _bubbleSelector.OnSelect += HandleSelect;
        }

        void OnDestroy()
        {
            _bubbleSelector.OnSelect -= HandleSelect;
        }

        void HandleSelect(GameObject original, GameObject replacement)
        {
            _bubbleWrap.PlaceBubble(replacement, original.GetComponent<BubbleBase>().gridPosition);

            // The bubble is now the new bubble type, So delete previous bubble type
            Destroy(original);
        }
    }
}