using UnityEngine.AI;

public class Member : SelectableMember
{
    public TeamManagement teamManagement;
    public int index;
    public UnitStateManager stateManager;
    public NavMeshAgent agent;
    public AgentLinkMover agentLinkMover;
    public AnimationHandler animationHandler;
    public MemberModel model;
    public MemberController controller;

    protected override void Awake()
    {
        base.Awake();
        agentLinkMover = GetComponent<AgentLinkMover>();
        model = GetComponent<MemberModel>();
        teamManagement = transform.parent.GetComponent<TeamManagement>();
    }

    public override void Select()
    {
        teamManagement.SelectOneUnit(index);
    }
}