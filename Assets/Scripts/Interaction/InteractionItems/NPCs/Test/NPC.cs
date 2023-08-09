using UnityEngine;
using UnityEngine.Events;

public class NPC : IInteraction
{
    [SerializeField] private UnityEvent<TextAsset,Member> onDialogueStart;
    public NPCModel npc;
    public Texture2D npcCursor;
    public override void Interaction(Member member)
    {
        onDialogueStart?.Invoke(npc.dialogue,member);
    }
}