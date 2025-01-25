using System.Collections;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class AreaBubble: BubbleBase
    {
        public AreaBubbleType bubbleType;
        
        private Bubbleplacer _bubbleplacer;
        
        public override void Pop()
        {
            base.Pop();

            StartCoroutine(PopNeighbours());
        }

        public void Stat()
        {
            _bubbleplacer = GetComponent<Bubbleplacer>();
        }
        
        
    
        IEnumerator PopNeighbours()
        {
            var delay = Random.Range(0.1f, 0.2f);
            
            foreach (var area in bubbleType.areas)
            {
                var percentile = Random.Range(0f, 1f);

                if (percentile < area.percentage)
                {
                    foreach (var bubbleObject in BubbleUtils.GetBubblesInArea(_bubbleplacer.CurrentPosition, area.Area))
                    {
                        yield return new WaitForSeconds(delay);
                        
                        bubbleObject.GetComponent<BubbleBase>().Pop();
                    }
                }
            }
        }
    }

}