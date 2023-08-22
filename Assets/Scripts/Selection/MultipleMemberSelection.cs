using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MultipleMemberSelection
{
    [SerializeField] private Camera cam;
    private Rect _selectionBox;
    [SerializeField] private RectTransform visualBox;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    public float sizeThreshold;
    public bool isDragging;

    [SerializeField] private float offsetY;
    [SerializeField] private float increment;

    private void Start()
    {
        _startPosition = Vector2.zero;
        _endPosition = Vector2.zero;
        DrawVisual();
        _selectionBox = new Rect();
    }

    public void SetStartPos(Vector2 pos)
    {
        _startPosition = pos;
    }

    public void SetEndPos(Vector2 pos)
    {
        _endPosition = pos;
    }

    public void DrawVisual()
    {
        visualBox.position = (_startPosition + _endPosition) / 2;

        Vector2 boxSize = new Vector2(Mathf.Abs(_startPosition.x - _endPosition.x), Mathf.Abs(_startPosition.y - _endPosition.y));

        visualBox.sizeDelta = boxSize;
        if (visualBox.sizeDelta.magnitude > sizeThreshold) isDragging = true;
    }

    public void DrawSelection()
    {
        if (Input.mousePosition.x < _startPosition.x)
        {
            _selectionBox.xMin = Input.mousePosition.x;
            _selectionBox.xMax = _startPosition.x;
        }
        else
        {
            _selectionBox.xMin = _startPosition.x;
            _selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < _startPosition.y)
        {
            _selectionBox.yMin = Input.mousePosition.y;
            _selectionBox.yMax = _startPosition.y;
        }
        else
        {
            _selectionBox.yMin = _startPosition.y;
            _selectionBox.yMax = Input.mousePosition.y;
        }
    }

    public void SelectMembers()
    {
        var indexes = new List<int>();
        Vector3 tempPos;
        bool found;

        foreach (var member in TeamManagement.Instance.members)
        {
            found = false;
            tempPos = member.transform.position;
            for (float i = 0; i <= offsetY; i+= increment)
            {
                tempPos.y += i;
                if (_selectionBox.Contains(cam.WorldToScreenPoint(tempPos)))
                {
                    found = true;
                    break;
                }   
            }
            
            if(found) indexes.Add(member.index);
        }
        if(indexes.Count != 0) TeamManagement.Instance.SelectMultipleUnit(indexes.ToArray());
        isDragging = false;
    }
}