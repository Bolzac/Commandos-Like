using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    public Clicker clicker;
    public MultipleSelector multipleSelector;
    public CursorToWorld cursorToWorld;
    public SingleSelector singleSelector;

    private bool isMultipleSelection;
    public UnityEvent<Vector2,Vector2> onMultipleSelection;
    public UnityEvent onMultipleSelectionDone;
    
    public float sizeThreshold;
    
    private Vector2 _selectionStartPos;
    private Vector2 _selectionLastPos;
    private Vector3 _startWorldPosition;
    private Vector3 _endWorldPosition;

    private float _selectionBoxSize;

    public void CountClicks()
    {
        clicker.ClickCounter();
    }

    public void Selection()
    {
        if(!CanSelect()) return;
         
        if (Input.GetMouseButtonDown(0))
        {
            _selectionStartPos = Input.mousePosition;
            _startWorldPosition = cursorToWorld.GetMouseWorldPosition(_selectionStartPos);
        }

        if (Input.GetMouseButton(0))
        {
            _selectionLastPos = Input.mousePosition;
            _selectionBoxSize = Mathf.Abs((_selectionStartPos.x - _selectionLastPos.x) * (_selectionStartPos.y - _selectionLastPos.y));
            if (_selectionBoxSize > sizeThreshold)
            {
                isMultipleSelection = true;
                onMultipleSelection?.Invoke(_selectionStartPos,_selectionLastPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endWorldPosition = cursorToWorld.GetMouseWorldPosition(_selectionLastPos);
            if (isMultipleSelection)
            {
                multipleSelector.MultipleSelection(_startWorldPosition,_endWorldPosition,_selectionStartPos,_selectionLastPos,cursorToWorld);
                onMultipleSelectionDone?.Invoke();
            }
            else if(Input.GetKey(KeyCode.LeftControl)) singleSelector.SingleSelection();
            else singleSelector.SingleSelection();

            isMultipleSelection = false;
        }
    }

    private bool CanSelect()
    {
        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return false;

        return true;
    }
}