using System;

public abstract class SelectableMember : SelectionBase
{
    public event Action<SelectableMember> OnSelect;
    public abstract void Select();

    protected override void Awake()
    {
        base.Awake();
        hoverState = HoverState.Member;
    }

    private void OnMouseDown()
    {
        if(!GameManager.Instance.isOverUI) OnSelect?.Invoke(this);
    }
}