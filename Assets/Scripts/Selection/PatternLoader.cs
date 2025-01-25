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
        
        public void Start()
        {
            Debug.Log("start of pattern loader");
            TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("BubbleConfigs");
            foreach (TextAsset file in jsonFiles)
            {
                AreaBubbleType areaBubbleType = JsonUtility.FromJson<AreaBubbleType>(file.text);
                
                areaBubbleTypes.Add(file.name, areaBubbleType);
            }
        }

        public AreaBubbleType GetPatternFromPath(string path)
        {
            return areaBubbleTypes.ContainsKey(path) ? areaBubbleTypes[path] : null;
        }

        public AreaBubbleType GetRandomPattern()
        {
            var allKeys = areaBubbleTypes.Keys;
            var randomKey = allKeys.ElementAt(UnityEngine.Random.Range(0, allKeys.Count));
            
            return GetPatternFromPath(randomKey);
        }
    }
}