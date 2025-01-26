using Selection;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Utils;

namespace DefaultNamespace
{
    public class BubblePopper: MonoBehaviour
    {
        private CurrentLevelContext _currentLevelContext;

        private bool      _poppingActive;
        private Texture2D pinTexture;
        private Texture2D mouseTexture;

        public void Start()
        {
            pinTexture           = Resources.Load<Texture2D>("Sprites/pin-popping");
            mouseTexture         = Resources.Load<Texture2D>("Sprites/mouse");
            _currentLevelContext = GameObject.Find("BubbleSelector").GetComponent<CurrentLevelContext>();
            _poppingActive       = false;
        }

        public void Update()
        {
            // Bubble is only poppable if selection is completed or poppiong currently active
            bool isPopMode = !(_currentLevelContext == null || !_currentLevelContext.IsBubbleSettingComplete || _poppingActive);
            UpdateCursor(isPopMode);
           
            if (isPopMode is false)
            {
                return;
            }

            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                BubbleBase bubbleBase = BubbleUtils.FindBubbleCollidingWith<BubbleBase>(pos);
                
                if (bubbleBase is null)
                    return;
                
                bubbleBase.Pop();
                _poppingActive = true;
            }
            
            void UpdateCursor(bool popModeActive)
            {
                if (popModeActive)
                {
                    Vector2 hotspot = new Vector2(0, pinTexture.height);
                    Cursor.SetCursor(pinTexture, hotspot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(mouseTexture, new Vector2(8, 0), CursorMode.Auto);
                }
            }
        }
    }
}