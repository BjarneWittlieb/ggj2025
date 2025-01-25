using System;
using Models;
using Selection;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class BubbleSelector : MonoBehaviour
{
    private bool _isSelectionActive;
    
    private BasicBubble _selectedBubble;
    private BubbleType _currentSelectionType;
    
    private ISelectionOverlay _currentSelectionOverlay;
    
    private PatternLoader _patternLoader;
    
    public event Action<BasicBubble> OnSelect;

    public event Action OnSelectionEnd;
    
    // TODO Rmove this, it's a helper to generate new patterns.
    private PatternGenerator _patternGenerator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _patternLoader = GetComponent<PatternLoader>();
        _patternGenerator = GetComponent<PatternGenerator>();
    }

    public void StartSelectionProcess()
    {
        // TODO make area bubble passable
        // _currentSelectionType = _patternLoader.GetRandomPattern();
        _currentSelectionType = _patternGenerator.GetPattern();
        
        _selectedBubble = null;
        _isSelectionActive = true;
    }

    private void EndSelectionProcess()
    {
        if (_selectedBubble != null)
        {
            Bubbleplacer placer = _selectedBubble.GetComponent<Bubbleplacer>();
            Debug.Log($"X: {placer.CurrentPosition.x}, y: {placer.CurrentPosition.y}");
            Debug.Log($"y % 2: {placer.CurrentPosition.y % 2}");
            OnSelect?.Invoke(_selectedBubble);
        }
        
        _isSelectionActive = false;

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
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _selectedBubble = BubbleUtils.FindBubbleCollidingWith<BasicBubble>(mousePosition);
        

        if (_selectedBubble != null)
        {
            transform.position = _selectedBubble.transform.position;
            
            RenderSelection();
        }
        else
        {
            DestroySelection();
        }
    }

    void RenderSelection()
    {
        DestroySelection();

        switch (_currentSelectionType)
        {
            case AreaBubbleType _:
                _currentSelectionOverlay = gameObject.AddComponent<AreaBubbleSelectionOverlay>();
                break;
        }
        
        _currentSelectionOverlay.Setup(_currentSelectionType);
        _currentSelectionOverlay.Render();
    }

    void DestroySelection()
    {
        // Already destroyed
        if (_currentSelectionOverlay == null)
        {
            return;
        }
        
        // First delete clean up overlay (Destroy all sub gameobjects instantiated)
        _currentSelectionOverlay.Destroy();
        
        // Then remove overlay component
        Destroy(GetComponentInChildren<AreaBubbleSelectionOverlay>());
        
        _currentSelectionOverlay = null;
    }
}
