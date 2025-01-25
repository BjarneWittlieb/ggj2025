using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utils;

namespace Selection
{
    public class AreaBubbleSelectionOverlay: MonoBehaviour, ISelectionOverlay
    {
        private AreaBubbleType _bubbleType;

        private GameObject _bubblePrefab;

        public void Setup(BubbleType bubbleType)
        {
            _bubbleType = (AreaBubbleType) bubbleType;
            _bubblePrefab = Resources.Load<GameObject>("Prefabs/SelectionBubble");
        }

        public void Render()
        {
            Bubbleplacer currentBubble = BubbleUtils.FindBubbleCollidingWith<Bubbleplacer>(transform.position);

            foreach (var area in _bubbleType.areas)
            {
                foreach (var surroundingBubble in BubbleUtils.GetBubblesInArea(currentBubble.CurrentPosition, area.Area))
                {
                    if (surroundingBubble == null)
                    {
                        continue;
                    }
                
                    GameObject selectionBubble = Instantiate(_bubblePrefab, surroundingBubble.transform.position, Quaternion.identity, transform);
                    selectionBubble.transform.parent = transform;
                    
                    SetOpacityOfBubbleOverlay(area.percentage, selectionBubble);
                }
            }
        }
        
        private void SetOpacityOfBubbleOverlay(float popPercentage, GameObject bubbleOveray)
        {
            SpriteRenderer renderer = bubbleOveray.GetComponent<SpriteRenderer>();
            var baseOpacity = renderer.color.a;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, baseOpacity * popPercentage);
        }
        
        public void Destroy()
        {
            // Destroy all children except the default one
            foreach (Transform child in transform)
            {
                if (child.name == "CenterSelection")
                {
                    continue;
                }
                Destroy(child.gameObject);
            }
        }
    }
}