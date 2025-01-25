using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class BubbleUtils
    {
        public static T FindBubbleCollidingWith<T>(Vector3 position)
        {
            Vector3 mappedPosition = position;
            mappedPosition.z = 0;
    
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(mappedPosition);

            foreach (Collider2D collider in hitColliders)
            {
                T bubble = collider.gameObject.GetComponent<T>();

                if (bubble == null)
                {
                    continue;
                }

                return bubble;
            }

            return default;
        }
        
            /// <summary>
            ///From a starting position get's all surrounding bubbles in the specefied pattern.
            ///
            /// We need to transform the pattern, since the y axis alternates between up and down.
            /// </summary>
            /// <param name="startPosition"></param>
            /// <param name="area"></param>
            /// <returns></returns>
        public static List<GameObject> GetBubblesInArea(Vector2Int startPosition, List<Vector2Int> area)
        {
            var bubbleWrap = GameObject.Find("grid").GetComponent<BubbleWrap>();

            // Don't touch this, it works! (@Corny)
            return area
                .Select(vec2 =>
                {
                    if (vec2.y % 2 == 0)
                    {
                        return bubbleWrap.GetBubble(startPosition + vec2);
                    }
                    
                    if (startPosition.y % 2 != 0)
                    {
                        return bubbleWrap.GetBubble(startPosition + vec2 + new Vector2Int(1, 0));
                    }
                    
                    return bubbleWrap.GetBubble(startPosition + vec2);
                })
                .Where(bubble => bubble != null)
                .ToList();
        }

    }
}