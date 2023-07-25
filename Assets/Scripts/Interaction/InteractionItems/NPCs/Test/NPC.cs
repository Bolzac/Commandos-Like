using System;
using UnityEngine;

public class NPC : IInteraction
{
    public NPCModel npc;
    public Texture2D npcCursor;
    public override void Interaction(Unit unit)
    {
        unit.model.isSpeaking = true;
        DialogueManager.instance.StartDialogue(npc.dialogue,unit);
    }

    private void OnMouseEnter()
    {
        CursorManager.instance.SetCursor(npcCursor);
    }

    private void OnMouseExit()
    {
        CursorManager.instance.ReturnDefaultCursor();
    }
}