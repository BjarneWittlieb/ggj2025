using System;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSelector : MonoBehaviour
{
    private bool _isSelectionActive;
    
    private SelectionOverlay _selectionOverlay;
    private BasicBubble _selectedBubble;
    
    public event Action<BasicBubble> OnSelect;

    public event Action OnSelectionEnd;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _selectionOverlay = GetComponentInChildren<SelectionOverlay>();
    }

    public void StartSelectionProcess()
    {
        Debug.Log("StartSelectionProcess");
        
        _selectedBubble = null;
        _isSelectionActive = true;
    }

    public void EndSelectionProcess()
    {
        if (_selectedBubble != null)
        {
            OnSelect?.Invoke(_selectedBubble);
        }
        
        _isSelectionActive = false;
        _selectionOverlay.gameObject.SetActive(false);

        OnSelectionEnd?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_isSelectionActive)
        {
            return;
        }

        if (Input.GetMouseButton((int) MouseButton.Left))
        {
            EndSelectionProcess();
            return;
        }
        
        _selectedBubble = FindHoveredBubble();

        if (_selectedBubble != null)
        {
            _selectionOverlay.gameObject.SetActive(true);
            _selectionOverlay.transform.position = _selectedBubble.transform.position;
        }
        else
        {
            _selectionOverlay.gameObject.SetActive(false);
        }
    }

    BasicBubble FindHoveredBubble()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePosition);

        Debug.Log("HIT? " + hitColliders.Length);
        
        foreach (Collider2D collider in hitColliders)
        {
            BasicBubble basicBubble = collider.gameObject.GetComponent<BasicBubble>();

            if (basicBubble == null)
            {
                continue;
            }

            return basicBubble;
        }

        return null;
    }
}
