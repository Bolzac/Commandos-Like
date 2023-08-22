using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionBase : MonoBehaviour
{
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
    }

    private void OnMouseExit()
    {
        if(!isClicked && !GameManager.Instance.isOverUI) SetOutline(false);
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