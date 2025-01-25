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
        private BubbleWrap _bubbleWrap;
        
        private AreaBubbleType _bubbleType;

        private GameObject _bubblePrefab;
        
        public void Start()
        {
            _bubbleWrap = GameObject.Find("grid").GetComponent<BubbleWrap>();
        }

        public void Update()
        {
            
        }

        public void Setup(BubbleType bubbleType)
        {
            _bubbleType = (AreaBubbleType) bubbleType;
            _bubblePrefab = Resources.Load<GameObject>("Prefabs/SelectionBubble");
        }

        public void Render()
        {
            _bubbleWrap = GameObject.Find("grid").GetComponent<BubbleWrap>();

            Bubbleplacer currentBubble = BubbleUtils.FindBubbleCollidingWith<Bubbleplacer>(transform.position);

            foreach (var area in _bubbleType.areas)
            {
                foreach (var surroundingBubble in GetBubblesInArea(currentBubble.CurrentPosition, area.Area))
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

        /// <summary>
        ///From a starting position get's all surrounding bubbles in the specefied pattern.
        ///
        /// We need to transform the pattern, since the y axis alternates between up and down.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private List<GameObject> GetBubblesInArea(Vector2Int startPosition, List<Vector2Int> area)
        {
            // Don't touch this, it works! (@Corny)
            return area
                .Select(vec2 =>
                {
                    if (vec2.y % 2 == 0)
                    {
                        return _bubbleWrap.GetBubble(startPosition + vec2);
                    }
                    
                    if (startPosition.y % 2 != 0)
                    {
                        return _bubbleWrap.GetBubble(startPosition + vec2 + new Vector2Int(1, 0));
                    }
                    
                    return _bubbleWrap.GetBubble(startPosition + vec2);
                })
                .Where(bubble => bubble != null)
                .ToList();
        }
        
        private void SetOpacityOfBubbleOverlay(float popPercentage, GameObject bubbleOveray)
        {
            SpriteRenderer renderer = bubbleOveray.GetComponent<SpriteRenderer>();
            var baseOpacity = renderer.color.a;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, baseOpacity * popPercentage);
        }
        
        public void Destroy()
        {
            // Destroy all children
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}