using UnityEngine;

namespace Interaction.InteractionItems.NPCs.Model
{
    [CreateAssetMenu(menuName = "NPC/Informations")]
    public class NPCModel : ScriptableObject
    {
        public string npcName;
        public TextAsset dialogue;
    }
}