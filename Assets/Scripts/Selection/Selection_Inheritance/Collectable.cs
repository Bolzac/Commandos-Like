using UnityEngine;
public class Collectable : Interactable
{
    
    protected override void Awake()
    {
        base.Awake();
        
    }

    public override void Interaction(Member member)
    {
        throw new System.NotImplementedException();
    }
}