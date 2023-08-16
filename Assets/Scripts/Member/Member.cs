using UnityEngine;
using UnityEngine.AI;

public class Member : MonoBehaviour
{
    public int index;
    public bool isMain;
    public UnitStateManager stateManager;
    public NavMeshAgent agent;
    public AgentLinkMover agentLinkMover;
    public AnimationHandler animationHandler;
    public MemberModel model;
    public MemberController controller;

    private void Awake()
    {
        agentLinkMover = GetComponent<AgentLinkMover>();
        model = GetComponent<MemberModel>();
    }
}