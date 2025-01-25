using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Selection
{
    public class CurrentLevelContext: MonoBehaviour
    {
        // DEPENDENCIES
        private PatternLoader _patternLoader;
        private BubbleSelector _bubbleSelector;
        
        // FIELDS
        private List<GameObject> _placeableBubbles = new ();
        private int _alreadyPlaced;

        
        public void Start()
        {
            _placeableBubbles = new List<GameObject>();
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
            
            const int level_size = 6;

            for (var i = 0; i < level_size; i++)
            {
                _placeableBubbles.Add(_patternLoader.GetRandomBubblePrefab());
            }
            
            Debug.Log("Level loaded!");
            
            // Subscribe to selection event => update already placed
            _bubbleSelector.OnSelect += PlaceBubble;
        }
        
        public GameObject GetCurrentBubble()
        {
            if (IsBubbleSettingComplete)
            {
                throw new ArgumentException("Tried getting next bubble, but all bubbles have been placed.");
            }
            
            var nextBubble = _placeableBubbles[_alreadyPlaced];
            
            return nextBubble;
        }

        private void PlaceBubble(GameObject original, GameObject replacement)
        {
            _alreadyPlaced++;
        }

        public void OnDestroy()
        {
            _bubbleSelector.OnSelect -= PlaceBubble;
        }
        
        public bool IsBubbleSettingComplete => _alreadyPlaced >= _placeableBubbles.Count;
    }
}