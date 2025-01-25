using System;
using Models;
using Selection;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class BubbleSelector : MonoBehaviour
{
    // INPUTS
    /// <summary>
    /// Set this to true to manually edit the pattern in the "PatternGenerator"
    /// </summary>
    [Tooltip("For manual pattern creation. If you don't now what this is, false is the right value.")]
    public bool manualPatternConfiguration = false;
    
    // FIELDS
    private bool _isSelectionActive;
    private BasicBubble _selectedBubble;
    private BubbleType _currentSelectionType;
    private ISelectionOverlay _currentSelectionOverlay;

    private Vector2Int _currentSelectedPosition;
    private Vector2Int _beforeSelectedPosition;
    
    // DEPENDENCIES
    private PatternLoader _patternLoader;
    // TODO Rmove this, it's a helper to generate new patterns.
    private PatternGenerator _patternGenerator;
    private LevelSelection _levelSelection;
    
    // OUTPUTS
    public event Action<GameObject, BubbleType> OnSelect;
    public event Action OnSelectionEnd;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _patternLoader = GetComponent<PatternLoader>();
        _patternGenerator = GetComponent<PatternGenerator>();
        _levelSelection = GetComponent<LevelSelection>();
    }

    public void StartSelectionProcess()
    {
        // manual selection only for pattern creation. In a level type setup this should always be false
        _currentSelectionType = manualPatternConfiguration ? 
            _patternGenerator.GetPattern() : 
            _levelSelection.GetCurrentBubble();
        
        _selectedBubble = null;
        _isSelectionActive = true;
    }

    private void EndSelectionProcess()
    {
        if (_selectedBubble != null)
        {
            OnSelect?.Invoke(_selectedBubble.gameObject, _currentSelectionType);
        }
        
        _isSelectionActive = false;
        OnSelectionEnd?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_levelSelection.IsBubbleSettingComplete)
        {
            return;
        }

        if (!_isSelectionActive)
        {
            StartSelectionProcess();
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
            _currentSelectedPosition = _selectedBubble.GetComponent<Bubbleplacer>().CurrentPosition;

            var newPos = new Vector3(_selectedBubble.transform.position.x, _selectedBubble.transform.position.y, transform.position.z);
            transform.position = newPos;
            
            RenderSelection();
        }
        else
        {
            // Set to impossible position so it will be rerendered next time
            _currentSelectedPosition = new Vector2Int(Int32.MaxValue, Int32.MaxValue);
            DestroySelection();
        }
    }

    void RenderSelection()
    {
        // Only trigger rerender if the current position changed 
        if (_currentSelectedPosition == _beforeSelectedPosition)
        {
            return;
        }
        
        DestroySelection();

        switch (_currentSelectionType)
        {
            case AreaBubbleType _:
                _currentSelectionOverlay = gameObject.AddComponent<AreaBubbleSelectionOverlay>();
                break;
        }
        
        _currentSelectionOverlay.Setup(_currentSelectionType);
        _currentSelectionOverlay.Render();
        
        _beforeSelectedPosition = _currentSelectedPosition;
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
