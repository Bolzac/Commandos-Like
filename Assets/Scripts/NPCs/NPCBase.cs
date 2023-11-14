using System;
using UnityEngine;

public abstract class NPCBase : Interactable
{
    public NPCModel npc;
    public  event Action<TextAsset,Member> OnDialogue;

    protected override void Awake()
    {
        base.Awake();
        hoverState = HoverState.NPC;
    }

    protected void ExecuteDialogue(Member member)
    {
        OnDialogue?.Invoke(npc.dialogue,member);
    }
}