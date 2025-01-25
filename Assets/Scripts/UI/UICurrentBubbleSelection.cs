using System.Collections;
using Models;
using Selection;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UICurrentBubbleSelection : MonoBehaviour
    {
        // DEPENDENCIES
        private BubbleSelector _bubbleSelector;
        private TMP_Text _textMeshPro;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _bubbleSelector = GameObject.Find("BubbleSelector")?.GetComponent<BubbleSelector>();
            _textMeshPro = GetComponentInChildren<TMP_Text>();
            
            if (_bubbleSelector == null)
            {
                Debug.LogError("BubbleSelector is null. A bubble selector must be connected in order for the selection display to work.");
            }
            else
            {
                // _bubbleSelector.OnSelect += HandleSelect;
                // StartCoroutine(SetTextAfterFrame());
            }
        }

        public void Update()
        {
            SetCurrentText();
        }

        private void HandleSelect(GameObject obj, BubbleType selection)
        {
            StartCoroutine(SetTextAfterFrame());
        }

        /// <summary>
        /// Has to be set after all OnSelect Events triggered. (UI Last)
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetTextAfterFrame()
        {
            yield return new WaitForEndOfFrame();
            
            SetCurrentText();
        }
        
        private void SetCurrentText()
        {
            if (!_bubbleSelector)
            {
                return;
            }
            
            CurrentLevelContext currentLevelContext = _bubbleSelector.GetComponent<CurrentLevelContext>();

            if (currentLevelContext.IsBubbleSettingComplete)
            {
                _textMeshPro.text = "All bubbles placed. Click to pop the first bubble!";
                return;
            }
            
            var currentBubble = currentLevelContext.GetCurrentBubble();
            
            _textMeshPro.text = "Current bubble:\n" + currentBubble.GetComponent<BubbleBase>().name;
        }

        public void OnDestroy()
        {
            // _bubbleSelector.OnSelect -= HandleSelect;
        }
    }
}
