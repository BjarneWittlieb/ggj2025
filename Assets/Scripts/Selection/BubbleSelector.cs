using System;
using Models;
using Selection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// BubblePlayer really
/// </summary>
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
    private BubbleBase _selectedBubble;
    private GameObject _currentSelectedBubblePrefab;

    private Vector2Int _oldSelectedPosition;

    // DEPENDENCIES
    private CurrentLevelContext _currentLevelContext;
    private ISelectionOverlay _currentSelectionOverlay;
    private BubbleWrap _bubbleWrap;
    private AudioSource _audioSource;
    
    // OUTPUTS
    public event Action<GameObject, GameObject> OnSelect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLevelContext = GetComponent<CurrentLevelContext>();
        _bubbleWrap = FindFirstObjectByType<BubbleWrap>();
        _audioSource = GetComponent<AudioSource>();
        
        this.transform.localScale = _bubbleWrap.transform.localScale;
    }

    public void StartSelectionProcess()
    {
        _isSelectionActive = true;

        // manual selection only for pattern creation. In a level type setup this should always be false
        _currentSelectedBubblePrefab = _currentLevelContext.GetCurrentBubble();
        _currentSelectedBubblePrefab = Instantiate(_currentSelectedBubblePrefab, transform.position, Quaternion.identity, transform);
        _currentSelectionOverlay  = _currentSelectedBubblePrefab.GetComponent<ISelectionOverlay>();

        _selectedBubble = null;
    }

    private void EndSelectionProcess()
    {
        if (_selectedBubble)
        {
            _audioSource.Play();
            OnSelect?.Invoke(_selectedBubble.gameObject, _currentSelectedBubblePrefab);
            _bubbleWrap.PlaceBubble(_currentSelectedBubblePrefab, _selectedBubble.gridPosition);
        }

        _currentSelectionOverlay?.Destroy();
        _isSelectionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentLevelContext.IsBubbleSettingComplete)
        {
            return;
        }

        if (Input.GetMouseButton((int) MouseButton.Left))
        {
            if (_isSelectionActive)
            {
                EndSelectionProcess();
            }

            return;
        }

        if (!_isSelectionActive)
        {
            StartSelectionProcess();
        }

        Vector3 mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        _selectedBubble = _bubbleWrap.GetBubble(mousePosition)?.GetComponent<BubbleBase>();

        if (_selectedBubble)
        {
            var currentPosition = _selectedBubble.gridPosition;
            var newPos = new Vector3(_selectedBubble.transform.position.x, _selectedBubble.transform.position.y, transform.position.z);
            _currentSelectedBubblePrefab.transform.position = newPos;

            RenderSelection(currentPosition);
        }
        else
        {
            // Set to impossible position so it will be rerendered next time
            _oldSelectedPosition = new Vector2Int(Int32.MaxValue, Int32.MaxValue);
            _currentSelectionOverlay?.Destroy();
        }
    }

    void RenderSelection(Vector2Int currentPosition)
    {
        if (currentPosition == _oldSelectedPosition)
        {
            return;
        }

        if (_currentSelectionOverlay != null)
        {
            _currentSelectionOverlay.Destroy();
            _currentSelectionOverlay.Render(currentPosition);
        }

        _oldSelectedPosition = currentPosition;
    }
}