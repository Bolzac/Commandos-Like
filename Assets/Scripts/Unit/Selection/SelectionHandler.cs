using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    public UnitManager unitManager;
    public Clicker clicker;
    public MultipleSelector multipleSelector;
    public CursorToWorld cursorToWorld;
    
    public float sizeThreshold;
    
    private Vector2 _selectionStartPos;
    private Vector2 _selectionLastPos;
    private Vector3 _startWorldPosition;
    private Vector3 _endWorldPosition;
    
    private SingleSelector _singleSelector;

    private float _selectionBoxSize;

    private void Awake()
    {
        unitManager = GetComponent<UnitManager>();
        _singleSelector = new SingleSelector();
    }

    private void Update()
    {   
        if (CanSelect()) clicker.ClickCounter(unitManager);
    }

    private void LateUpdate()
    {
        if (CanSelect()) Selection();
    }
    
    private void Selection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectionStartPos = Input.mousePosition;
            _startWorldPosition = cursorToWorld.GetMouseWorldPosition(_selectionStartPos);
        }

        if (Input.GetMouseButton(0))
        {
            _selectionLastPos = Input.mousePosition;
            _selectionBoxSize = Mathf.Abs((_selectionStartPos.x - _selectionLastPos.x) * (_selectionStartPos.y - _selectionLastPos.y));
            if (_selectionBoxSize > sizeThreshold) UIManager.instance.selectionBoxHandler.CreateSelectionBox(_selectionStartPos,_selectionLastPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endWorldPosition = cursorToWorld.GetMouseWorldPosition(_selectionLastPos);
            if (UIManager.instance.selectionBoxHandler.selectionBox.gameObject.activeSelf)
            {
                multipleSelector.MultipleSelection(_startWorldPosition,_endWorldPosition,_selectionStartPos,_selectionLastPos,unitManager,cursorToWorld);
                UIManager.instance.selectionBoxHandler.DisableSelectionBox();
            }
            else if(Input.GetKey(KeyCode.LeftControl)) _singleSelector.SingleSelection(unitManager);
            else _singleSelector.SingleSelection(unitManager);
        }
    }

    private bool CanSelect()
    {
        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return false;
        if(DialogueManager.instance.DialogueIsPlaying) return false;
        if (unitManager.selectedUnits[0].model.isSpeaking) return false;

        return true;
    }
}