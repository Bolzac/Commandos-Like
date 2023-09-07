using UnityEngine;
public class MoveTo : Command<Member>
{
    public Vector3 destination;
    public float destinationThreshold;
    public MoveTo(Member member) : base(member) {}
    public override void Start()
    {
        runner.agent.SetDestination(destination);
    }

    public override bool IsFinished()
    {
        return runner.agent.destination == runner.transform.position;
    }
}