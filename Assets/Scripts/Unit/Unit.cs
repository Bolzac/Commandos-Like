using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int index;
    
    public UnitManager unitManager;
    public UnitStateManager stateManager;
    public NavMeshAgent agent;
    public AnimationHandler animationHandler;
    public UnitModel model;
    public UnitController controller;

    private void Update()
    {
        animationHandler.SetPatrolBlend(agent.velocity.magnitude);
    }

    public void Init(UnitManager manager)
    {
        unitManager = manager;
    }
}