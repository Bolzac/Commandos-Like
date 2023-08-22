using UnityEngine;
using UnityEngine.Events;

public class NPC : Interactable
{
    [SerializeField] private UnityEvent<TextAsset,Member> onDialogueStart;
    public NPCModel npc;
    public override void Interaction(Member member)
    {
        onDialogueStart?.Invoke(npc.dialogue,member);
    }
}