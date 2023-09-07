using UnityEngine;

public class SelectionBase : MonoBehaviour
{
    [HideInInspector] public HoverState hoverState;
    private Outline outline;
    protected bool isClicked;
    
    protected virtual void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        isClicked = false;
    }

    private void OnMouseEnter()
    {
        if(!GameManager.Instance.isOverUI) SetOutline(true);
        CursorManager.Instance.SetCursor(hoverState);
    }

    private void OnMouseExit()
    {
        if(!isClicked && !GameManager.Instance.isOverUI) SetOutline(false);
        CursorManager.Instance.ReturnDefaultCursor();
    }

    public void SetOutline(bool enable)
    {
        outline.enabled = enable;
    }

    public void SetClicked(bool click)
    {
        isClicked = click;
    }
}

public enum HoverState
{
    Other,
    Member,
    Enemy,
    NPC,
}