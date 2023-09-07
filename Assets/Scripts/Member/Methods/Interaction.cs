public class Interaction : Command<Member>
{
    public Interactable interactable;
    public Interaction(Member member) : base(member) {}
    public override void Start()
    {
        runner.transform.LookAt(interactable.transform.position);
        interactable.Interaction(runner);
    }

    public override bool IsFinished()
    {
        return runner.animationHandler.IsActionDone();
    }
}