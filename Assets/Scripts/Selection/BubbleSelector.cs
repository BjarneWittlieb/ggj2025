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
    private GameObject _currentSelectedBubblePrefab;
    private ISelectionOverlay _currentSelectionOverlay;

    private Vector2Int _beforeSelectedPosition;
    
    // DEPENDENCIES
    private PatternLoader _patternLoader;
    // TODO Rmove this, it's a helper to generate new patterns.
    private PatternGenerator _patternGenerator;
    private CurrentLevelContext _currentLevelContext;
    
    // OUTPUTS
    public event Action<GameObject, GameObject> OnSelect;
    public event Action OnSelectionEnd;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _patternLoader = GetComponent<PatternLoader>();
        _patternGenerator = GetComponent<PatternGenerator>();
        _currentLevelContext = GetComponent<CurrentLevelContext>();
    }

    public void StartSelectionProcess()
    {
        _isSelectionActive = true;
        
        // manual selection only for pattern creation. In a level type setup this should always be false
        _currentSelectedBubblePrefab = _currentLevelContext.GetCurrentBubble();
        _currentSelectedBubblePrefab = Instantiate(_currentSelectedBubblePrefab, transform.position, Quaternion.identity, transform);
        
        _selectedBubble = null;
    }

    private void EndSelectionProcess()
    {
        if (_selectedBubble != null)
        {
            OnSelect?.Invoke(_selectedBubble.gameObject, _currentSelectedBubblePrefab);
        }
        
        _isSelectionActive = false;
        OnSelectionEnd?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_currentLevelContext.IsBubbleSettingComplete)
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
            var currentPosition = _selectedBubble.gridPosition;
            var newPos = new Vector3(_selectedBubble.transform.position.x, _selectedBubble.transform.position.y, transform.position.z);
            transform.position = newPos;
            
            RenderSelection(currentPosition);
        }
        else
        {
            _beforeSelectedPosition = new Vector2Int(Int32.MaxValue, Int32.MaxValue);
            // Set to impossible position so it will be rerendered next time
            DestroySelection();
        }
    }

    void RenderSelection(Vector2Int currentPosition)
    {
        // Only trigger rerender if the current position changed 
        if (currentPosition == _beforeSelectedPosition)
        {
            return;
        }
        
        DestroySelection();

        _currentSelectionOverlay = _currentSelectedBubblePrefab.GetComponent<ISelectionOverlay>();

        if (_currentSelectionOverlay != null)
        {
            _currentSelectionOverlay.Render();
        }
        
        _beforeSelectedPosition = currentPosition;
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
        // Destroy(GetComponentInChildren<AreaBubbleSelectionOverlay>());
        
        _currentSelectionOverlay = null;
    }
}
