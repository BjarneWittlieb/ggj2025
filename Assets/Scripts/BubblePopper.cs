using System;
using Selection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Utils;

namespace DefaultNamespace
{
    public class BubblePopper: MonoBehaviour
    {
        private CurrentLevelContext _currentLevelContext;

        private bool _poppingActive;
        
        public void Start()
        {
             _currentLevelContext = GameObject.Find("BubbleSelector").GetComponent<CurrentLevelContext>();
             
             _poppingActive = false;
        }

        public void Update()
        {
            // Bubble is only poppable if selection is completed or poppiong currently active
            if (_currentLevelContext == null || !_currentLevelContext.IsBubbleSettingComplete || _poppingActive)
            {
                return;
            }

            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                BubbleBase bubbleBase = BubbleUtils.FindBubbleCollidingWith<BubbleBase>(pos);
                
                bubbleBase.Pop();
                _poppingActive = true;
            }
            
        }
    }
}