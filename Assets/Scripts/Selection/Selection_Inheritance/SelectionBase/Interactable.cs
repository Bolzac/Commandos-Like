using System;

public abstract class Interactable : SelectionBase
{
    public string interactionAnimation;
    public float interactionDistance;
    public event Action<Interactable> OnInteract;
    public abstract void Interaction(Member member);

    private void OnMouseDown()
    {
        if(!isClicked) OnInteract?.Invoke(this);
    }
}