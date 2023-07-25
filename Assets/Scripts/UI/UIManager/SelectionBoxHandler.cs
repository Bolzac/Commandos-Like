using System;
using UnityEngine;

[Serializable]
public class SelectionBoxHandler
{
    public RectTransform selectionBox;
    
    public void CreateSelectionBox(Vector3 selectionStartPos,Vector3 selectionLastPos)
    {
        selectionBox.gameObject.SetActive(true);
        selectionBox.position = selectionStartPos;
        if (selectionStartPos.x < selectionLastPos.x && selectionStartPos.y > selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.up;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(selectionStartPos.x - selectionLastPos.x),
                Mathf.Abs(selectionStartPos.y - selectionLastPos.y));   
        }else if (selectionStartPos.x > selectionLastPos.x && selectionStartPos.y < selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.right;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(selectionLastPos.x - selectionStartPos.x),
                Mathf.Abs(selectionLastPos.y - selectionStartPos.y));
        }else if (selectionStartPos.x > selectionLastPos.x && selectionStartPos.y > selectionLastPos.y)
        {
            selectionBox.pivot = Vector2.one;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(selectionLastPos.x - selectionStartPos.x),
                Mathf.Abs(selectionLastPos.y - selectionStartPos.y));
        }
        else
        {
            selectionBox.pivot = Vector2.zero;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(selectionLastPos.x - selectionStartPos.x),
                Mathf.Abs(selectionLastPos.y - selectionStartPos.y));
        }
    }
    
    public void DisableSelectionBox()
    {
        selectionBox.sizeDelta = Vector2.zero;
        selectionBox.gameObject.SetActive(false);
    }
}