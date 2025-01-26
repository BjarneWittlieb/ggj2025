using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;
using Quaternion = UnityEngine.Quaternion;

namespace Selection
{
    public class AreaBubbleSelectionOverlay: MonoBehaviour, ISelectionOverlay
    {
        private AreaBubble _areaBubble;
        private GameObject _bubblePreviewPrefab;
        private List<GameObject> _previewBubbles = new();

        private void Start()
        {
            _bubblePreviewPrefab = Resources.Load<GameObject>("Prefabs/SelectionBubble");
            _areaBubble          = GetComponent<AreaBubble>();
        }

        public void Render(Vector2Int gridPosition)
        {
            foreach (var area in _areaBubble?.config?.areas ?? Array.Empty<BubbleAreaWithPercentage>())
            {
                foreach (var surroundingBubble in BubbleUtils.GetBubblesInArea(gridPosition, area.Area))
                {
                    if (!surroundingBubble)
                    {
                        continue;
                    }

                    var position = surroundingBubble.transform.position;
                    position.z = transform.position.z;
                    GameObject previewBubble = Instantiate(_bubblePreviewPrefab, position, Quaternion.identity);
                    _previewBubbles.Add(previewBubble);
                    SetOpacityOfBubbleOverlay(area.percentage, previewBubble);
                    SetupWobbleOfOverlay(previewBubble, surroundingBubble);
                }
            }
        }

        private void SetOpacityOfBubbleOverlay(float popPercentage, GameObject bubbleOverlay)
        {
            SpriteRenderer renderer = bubbleOverlay.GetComponent<SpriteRenderer>();
            var baseOpacity = renderer.color.a;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, baseOpacity * popPercentage);
        }

        private void SetupWobbleOfOverlay(GameObject bubbleOverlay, GameObject originalBubble)
        {
            var bubbleWobble = originalBubble.GetComponent<BubbleWobble>();
            var highlighting = bubbleOverlay.GetComponent<BubbleHighlighting>();

            highlighting.BubbleWobble = bubbleWobble;
            highlighting.AdjustToBubble = true;
        }

        public void Destroy()
        {
            foreach (GameObject preview in _previewBubbles)
            {
                Destroy(preview);
            }
        }
    }
}