using System;
using UnityEngine;

public abstract class Interactable : SelectionBase
{
    public event Action<Interactable> OnInteract;
    public abstract void Interaction(Member member);

    private void OnMouseDown()
    {
        if(!isClicked) OnInteract?.Invoke(this);
    }
}