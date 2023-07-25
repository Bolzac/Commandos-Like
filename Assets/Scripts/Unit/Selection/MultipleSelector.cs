using System;
using UnityEngine;
[Serializable]
public class MultipleSelector
{
    private CursorToWorld _cursorToWorld;
    
    public LayerMask unitLayer;

    private UnitManager _unitManager;
    private Vector2 _firstVertex;
    private Vector2 _secondVertex;
    
    private Vector3 _firstVertexWorldPos;
    private Vector3 _secondVertexWorldPos;

    public void MultipleSelection(Vector3 startWorldPosition, Vector3 endWorldPosition,Vector3 selectionStartPos,Vector3 selectionLastPos, UnitManager unitManager, CursorToWorld cursorToWorld)
    {
        _cursorToWorld = cursorToWorld;
        _unitManager = unitManager;
        SetVertices(selectionStartPos,selectionLastPos);
        CreateDetectionBox(startWorldPosition,endWorldPosition,true);
        CreateDetectionBox(_firstVertexWorldPos,_secondVertexWorldPos,false);
    }
    
    private void SetVertices(Vector3 selectionStartPos,Vector3 selectionLastPos)
    {
        _firstVertex = new Vector2(selectionStartPos.x, selectionLastPos.y);
        _secondVertex = new Vector2(selectionLastPos.x, selectionStartPos.y);
        _firstVertexWorldPos = _cursorToWorld.GetMouseWorldPosition(_firstVertex);
        _secondVertexWorldPos = _cursorToWorld.GetMouseWorldPosition(_secondVertex);
    }
    
    private void CreateDetectionBox(Vector3 worldPosOne,Vector3 worldPosTwo,bool clear)
    {
        var colliders = new Collider[4];
        var halfExtents = (worldPosOne - worldPosTwo) / 2f;
        halfExtents = new Vector3(Mathf.Abs(halfExtents.x), Mathf.Abs(halfExtents.y), Mathf.Abs(halfExtents.z));
        if (halfExtents.y < 3) halfExtents.y = 3;
        
        var size = Physics.OverlapBoxNonAlloc((worldPosOne + worldPosTwo) / 2f, halfExtents, colliders,
            Quaternion.identity, unitLayer);

        if (size > 0 && clear) _unitManager.ClearAllSelected();

        switch (size)
        {
            case 1:
            {
                if(colliders[0].TryGetComponent(out Unit unit)) _unitManager.SelectOneUnit(unit.index);
                break;
            }
            case > 1:
            {
                int[] indexes = new int[size];
                for (var i = 0; i < size; i++)
                {
                    if (colliders[i].TryGetComponent(out Unit unit))
                    {
                        indexes[i] = unit.index;
                    }
                }
                _unitManager.SelectMultipleUnit(indexes);
                break;
            }
        }
    }
}