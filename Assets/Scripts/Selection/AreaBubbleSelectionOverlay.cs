using System.Numerics;
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

        private void Start()
        {
            _bubblePreviewPrefab = Resources.Load<GameObject>("Prefabs/SelectionBubble");
            _areaBubble          = GetComponent<AreaBubble>();
        }

        public void RenderPlacementPreview(Vector2Int position)
        {
            BubbleBase centerBubble = BubbleUtils.FindBubbleCollidingWith<BubbleBase>(transform.position);
        }
        
        public void Render()
        {
            BubbleBase currentBubble = BubbleUtils.FindBubbleCollidingWith<BubbleBase>(transform.position);

            foreach (var area in _areaBubble.config.areas)
            {
                foreach (var surroundingBubble in BubbleUtils.GetBubblesInArea(currentBubble.gridPosition, area.Area))
                {
                    if (!surroundingBubble)
                    {
                        continue;
                    }

                    var position = surroundingBubble.transform.position;
                    position.z = transform.position.z;
                    GameObject previewBubble = Instantiate(_bubblePreviewPrefab, position, Quaternion.identity, transform);
                    
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
            
            Debug.Log(bubbleWobble);
            Debug.Log(highlighting);
            
            highlighting.BubbleWobble = bubbleWobble;
            highlighting.AdjustToBubble = true;
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