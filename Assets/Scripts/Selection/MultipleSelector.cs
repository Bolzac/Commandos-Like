using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MultipleSelector
{
    public UnityEvent<int> onSelectOneUnit;
    public UnityEvent<int[]> onSelectMultipleUnit;
    public UnityEvent onClear;
    
    private CursorToWorld _cursorToWorld;
    
    public LayerMask unitLayer;
    
    private Vector2 _firstVertex;
    private Vector2 _secondVertex;
    
    private Vector3 _firstVertexWorldPos;
    private Vector3 _secondVertexWorldPos;

    public void MultipleSelection(Vector3 startWorldPosition, Vector3 endWorldPosition,Vector3 selectionStartPos,Vector3 selectionLastPos, CursorToWorld cursorToWorld)
    {
        _cursorToWorld = cursorToWorld;
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

        if (size > 0 && clear) onClear?.Invoke(); //TeamManagement.ClearAllSelected

        switch (size)
        {
            case 1:
            {
                if(colliders[0].TryGetComponent(out Member unit)) onSelectOneUnit?.Invoke(unit.index); //TeamManagement.SelectOneMember
                break;
            }
            case > 1:
            {
                int[] indexes = new int[size];
                for (var i = 0; i < size; i++)
                {
                    if (colliders[i].TryGetComponent(out Member unit))
                    {
                        indexes[i] = unit.index;
                    }
                }
                onSelectMultipleUnit?.Invoke(indexes); //TeamManagement.SelectMultipleMember
                break;
            }
        }
    }
}