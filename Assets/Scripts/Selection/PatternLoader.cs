using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class PatternLoader: MonoBehaviour
    {
        private Dictionary<String, AreaBubbleType> areaBubbleTypes = new Dictionary<String, AreaBubbleType>();
        
        public void Start()
        {
            TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("BubbleConfigs");
            Debug.Log($"LOaded BubbleConfigs: {jsonFiles.Length}");
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
            Debug.Log(allKeys.Count);
            var randomKey = allKeys.ElementAt(UnityEngine.Random.Range(0, allKeys.Count));
            
            return GetPatternFromPath(randomKey);
        }
    }
}