using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    public UnitManager unitManager;

    #region SelectionBox
    
    private float _selectionBoxSize;
    [Header("SelectionBox")]
    public float sizeThreshold;
    public RectTransform selectionBox;

    #endregion
    #region ScreenPositions
    
    private Vector2 _selectionStartPos;
    private Vector2 _selectionLastPos;
    private Vector2 _firstVertex;
    private Vector2 _secondVertex;

    #endregion
    #region WorldPositions

    private Vector3 _startWorldPosition;
    private Vector3 _endWorldPosition;
    private Vector3 _firstVertexWorldPos;
    private Vector3 _secondVertexWorldPos;

    #endregion
    #region Click
    
    private int _clickTime;
    public float clickSpacing;
    private float _counter;

    #endregion
    #region Layers

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask unitLayer;
    
    #endregion
    
    private bool _isStarted;

    private void Awake()
    {
        unitManager = GetComponent<UnitManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            unitManager.teamController.DisableRunning();
            _clickTime++;
        }
        ClickCounter();
    }

    private void LateUpdate()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            _selectionStartPos = Input.mousePosition;
            _startWorldPosition = GetMouseWorldPosition(_selectionStartPos);
            _isStarted = true;
        }

        if (_isStarted && Input.GetMouseButton(0))
        {
            _selectionLastPos = Input.mousePosition;
            _selectionBoxSize = Mathf.Abs((_selectionStartPos.x - _selectionLastPos.x) * (_selectionStartPos.y - _selectionLastPos.y));
            if (_selectionBoxSize > sizeThreshold) SetSelectionBox();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endWorldPosition = GetMouseWorldPosition(_selectionLastPos);
            _isStarted = false;
            if (selectionBox.gameObject.activeSelf)
            {
                MultipleSelection();
                DisableSelectionBox();
            }
            else if(Input.GetKey(KeyCode.LeftControl)) SingleSelection(false);
            else SingleSelection(true);
        }
    }

    private void SetSelectionBox()
    {
        selectionBox.gameObject.SetActive(true);
        selectionBox.position = _selectionStartPos;
        if (_selectionStartPos.x < _selectionLastPos.x && _selectionStartPos.y > _selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.up;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(_selectionStartPos.x - _selectionLastPos.x),
                Mathf.Abs(_selectionStartPos.y - _selectionLastPos.y));   
        }else if (_selectionStartPos.x > _selectionLastPos.x && _selectionStartPos.y < _selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.right;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(_selectionLastPos.x - _selectionStartPos.x),
                Mathf.Abs(_selectionLastPos.y - _selectionStartPos.y));
        }else if (_selectionStartPos.x > _selectionLastPos.x && _selectionStartPos.y > _selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.one;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(_selectionLastPos.x - _selectionStartPos.x),
                Mathf.Abs(_selectionLastPos.y - _selectionStartPos.y));
        }
        else
        {
            selectionBox.pivot = Vector2.zero;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(_selectionLastPos.x - _selectionStartPos.x),
                Mathf.Abs(_selectionLastPos.y - _selectionStartPos.y));
        }
    }

    private void DisableSelectionBox()
    {
        selectionBox.sizeDelta = Vector2.zero;
        selectionBox.gameObject.SetActive(false);
    }

    private void SingleSelection(bool clear)
    {
        var ray = unitManager.cam.ScreenPointToRay(_selectionLastPos);
        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent(out Unit unit)) unitManager.AddOneUnit(clear,unit);
            else
            {
                switch (hit.transform.tag)
                {
                    case "Ground":
                        unitManager.teamController.AssignDestinations(hit.point);
                        break;
                    case "Interaction":
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void MultipleSelection()
    {
        SetVertices();
        CreateDetectionBox(_startWorldPosition,_endWorldPosition,true);
        CreateDetectionBox(_firstVertexWorldPos,_secondVertexWorldPos,false);
    }

    private void CreateDetectionBox(Vector3 worldPosOne,Vector3 worldPosTwo,bool clear)
    {
        var colliders = new Collider[4];
        var halfExtents = (worldPosOne - worldPosTwo) / 2f;
        halfExtents = new Vector3(Mathf.Abs(halfExtents.x), Mathf.Abs(halfExtents.y), Mathf.Abs(halfExtents.z));
        if (halfExtents.y < 3) halfExtents.y = 3;
        
        var size = Physics.OverlapBoxNonAlloc((worldPosOne + worldPosTwo) / 2f, halfExtents, colliders,
            Quaternion.identity, unitLayer);
        
        if (size > 0)
        {
            if (clear)
            {
                unitManager.ClearAllSelected();
                unitManager.selectedUnits.Clear();   
            }
            for (int i = 0; i < size; i++)
            {
                if(colliders[i].TryGetComponent(out Unit unit)) unitManager.AddOneUnit(false,unit);
            }
        }
    }

    private void SetVertices()
    {
        _firstVertex = new Vector2(_selectionStartPos.x, _selectionLastPos.y);
        _secondVertex = new Vector2(_selectionLastPos.x, _selectionStartPos.y);
        _firstVertexWorldPos = GetMouseWorldPosition(_firstVertex);
        _secondVertexWorldPos = GetMouseWorldPosition(_secondVertex);
    }


    private Vector3 GetMouseWorldPosition(Vector3 pos)
    {
        Ray ray = unitManager.cam.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,Mathf.Infinity,groundLayer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private void ClickCounter()
    {
        if (_clickTime > 0)
        {
            _counter += Time.deltaTime;
            if (_counter >= clickSpacing)
            {
                _counter = 0;
                _clickTime = 0;
            }
            else if(_clickTime == 2)
            {
                unitManager.teamController.HandleRunning();
                _counter = 0;
                _clickTime = 0;
            }
        }
    }
}