using UnityEngine;

namespace Selection
{
    public class PatternLoader: MonoBehaviour
    {
        [SerializeField]
        public GameObject[] bubblePrefabs;

        public void Start()
        {
            Debug.Log("start of pattern loader");
            
            if (bubblePrefabs.Length == 0)
                bubblePrefabs = Resources.LoadAll<GameObject>("Prefabs/BubbleTypes");
        }
        
        public GameObject GetRandomBubblePrefab()
        {
            return bubblePrefabs[Random.Range(0, bubblePrefabs.Length)];
        }
    }
}