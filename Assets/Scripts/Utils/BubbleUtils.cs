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
    }
}