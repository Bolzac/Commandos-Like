using UnityEngine;

public class StandUp : Command<Member>
{
    private readonly int _crouch = Animator.StringToHash("Crouch");
    public StandUp(Member member) : base(member) {}
    public override void Start()
    {
        runner.animationHandler.animator.SetBool(_crouch,false);
        runner.model.isCrouching = false;
    }

    public override bool IsFinished()
    {
        return true;
    }
}