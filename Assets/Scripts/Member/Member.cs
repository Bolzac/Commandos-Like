using UnityEngine;
using UnityEngine.AI;

public class Member : SelectableMember
{
    [HideInInspector] public int index;
    public UnitStateManager stateManager;
    public NavMeshAgent agent;
    public AgentLinkMover agentLinkMover;
    public AnimationHandler animationHandler;
    public MemberModel model;
    public MemberController controller;
    public CommandManager commandManager;

    public override void Select()
    {
        TeamManagement.Instance.SelectOneUnit(index);
    }
}