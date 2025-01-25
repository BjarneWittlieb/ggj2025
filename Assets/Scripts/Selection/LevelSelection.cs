using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Selection
{
    public class LevelSelection: MonoBehaviour
    {
        // DEPENDENCIES
        private PatternLoader _patternLoader;
        private BubbleSelector _bubbleSelector;
        
        // FIELDS
        private List<BubbleType> _placeableBubbles = new ();
        private int _alreadyPlaced;

        // OUTPUTS
        public event Action AllBubblesPlaced;
        
        public void Start()
        {
            _placeableBubbles = new List<BubbleType>();
            _alreadyPlaced = 0;
            
            _patternLoader = GetComponent<PatternLoader>();
            _bubbleSelector = GetComponent<BubbleSelector>();

            StartCoroutine(LoadBubbleTypesForLevel());
        }

        /// <summary>
        /// This must happen after initialization of the patternLoader.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadBubbleTypesForLevel()
        {
            yield return new WaitForEndOfFrame();
            
            // TODO level is loadable?
            Debug.Log("Whaddup");
            
            const int level_size = 6;

            for (var i = 0; i < level_size; i++)
            {
                _placeableBubbles.Add(_patternLoader.GetRandomPattern());
            }
            
            Debug.Log(_placeableBubbles.Count);
            
            // Subscribe to selection event => update already placed
            _bubbleSelector.OnSelect += PlaceBubble;
        }
        
        public BubbleType GetCurrentBubble()
        {
            if (IsBubbleSettingComplete)
            {
                throw new ArgumentException("Tried getting next bubble, but all bubbles have been placed.");
            }
            
            var nextBubble = _placeableBubbles[_alreadyPlaced];
            
            return nextBubble;
        }

        private void PlaceBubble(GameObject bubble, BubbleType bubbleType)
        {
            _alreadyPlaced++;

            if (IsBubbleSettingComplete)
            {
                AllBubblesPlaced?.Invoke();
            }
        }

        public void OnDestroy()
        {
            _bubbleSelector.OnSelect -= PlaceBubble;
        }
        
        private bool IsBubbleSettingComplete => _alreadyPlaced >= _placeableBubbles.Count;
    }
}