using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public UnitManager unitManager;
    public UnitStateManager stateManager;
    public NavMeshAgent agent;
    public AnimationHandler animationHandler;
    public UnitModel model;
    public UnitController controller;

    private void Awake()
    {
        stateManager = GetComponent<UnitStateManager>();
        animationHandler = transform.GetChild(0).GetComponent<AnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        model = GetComponent<UnitModel>();
        controller = GetComponent<UnitController>();
    }

    private void Update()
    {
        animationHandler.SetPatrolBlend(agent.velocity.magnitude);
    }

    public void Init(UnitManager manager)
    {
        unitManager = manager;
    }
}