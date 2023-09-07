public class NPC : NPCBase
{
    public override void Interaction(Member member)
    {
        ExecuteDialogue(member);
    }
}