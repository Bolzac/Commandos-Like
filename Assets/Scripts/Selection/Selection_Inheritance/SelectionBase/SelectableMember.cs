using System;
using UnityEngine;

public abstract class SelectableMember : SelectionBase
{
    public event Action<SelectableMember> OnSelect;
    public abstract void Select();

    private void OnMouseDown()
    {
        if(!GameManager.Instance.isOverUI) OnSelect?.Invoke(this);
    }
}