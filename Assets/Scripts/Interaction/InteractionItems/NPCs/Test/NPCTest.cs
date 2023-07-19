using Interaction.InteractionItems.NPCs.Model;
using UnityEngine;

public class NPCTest : IInteraction
{
    public NPCModel npc;
    public override void Interaction()
    {
        DialogueManager.instance.StartDialogue(npc.npcName,npc.dialogue);
    }
}