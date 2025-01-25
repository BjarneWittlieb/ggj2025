using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Selection
{
    public class PatternLoader: MonoBehaviour
    {
        private Dictionary<String, AreaBubbleType> areaBubbleTypes = new ();

        private GameObject[] bubblePrefabs;
        
        public void Start()
        {
            Debug.Log("start of pattern loader");
            bubblePrefabs = Resources.LoadAll<GameObject>("Prefabs/BubbleTypes");
        }
        
        public GameObject GetRandomBubblePrefab()
        {
            return bubblePrefabs[UnityEngine.Random.Range(0, bubblePrefabs.Length)];
        }
    }
}