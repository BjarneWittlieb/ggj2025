using UnityEditor.Analytics;
using UnityEngine;

namespace Selection
{
    public class BubbleHighlighting : MonoBehaviour
    {
        public BubbleWobble BubbleWobble { private get; set; }

        private BubbleWrap _bubbleWrap;
        
        private float _baseScale;
        
        public bool AdjustToBubble
        {
            private get;
            set;
        }
        
        // Start is called once b
        void Start()
        {
            _bubbleWrap = GameObject.Find("grid").GetComponent<BubbleWrap>();
            _baseScale = transform.localScale.x * _bubbleWrap.transform.localScale.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (BubbleWobble && AdjustToBubble)
            {
                // TODO the number at the end should be the default scaling of the basic bubble
                transform.localScale = BubbleWobble.transform.localScale * (_baseScale * (1f / 0.4837665f));
            }
        }
    }
}
