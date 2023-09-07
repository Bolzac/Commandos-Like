using UnityEngine;

public class Crouch : Command<Member>
{
    private readonly int _crouch = Animator.StringToHash("Crouch");
    public Crouch(Member member) : base(member) {}
    public override void Start()
    {
        runner.animationHandler.animator.SetBool(_crouch,true);
        runner.model.isRunning = false;
        runner.model.isCrouching = true;
    }

    public override bool IsFinished()
    {
        return true;
    }
}