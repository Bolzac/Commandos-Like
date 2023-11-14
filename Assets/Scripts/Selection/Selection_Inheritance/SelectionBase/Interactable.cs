using System;

public abstract class Interactable : SelectionBase
{
    public string interactionAnimation;
    public float interactionDistance;
    public event Action<Interactable> OnInteract;
    public abstract void Interaction(Member member);

    private void OnMouseDown()
    { 
        if(InGameManager.Instance.currentState.GetType() == typeof(DialogueState)) return;
        if(!isClicked && !GameManager.Instance.isOverUI) OnInteract?.Invoke(this);
    }
}