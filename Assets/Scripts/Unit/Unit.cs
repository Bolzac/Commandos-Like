using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public UnitManager unitManager;
    public InputHandler inputHandler;
    public NavMeshAgent agent;
    public AnimationHandler animationHandler;
    public UnitStateManager stateMachine;
    public UnitModel model;
    public UnitController controller;

    private void Awake()
    {
        animationHandler = transform.GetChild(0).GetComponent<AnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        model = GetComponent<UnitModel>();
        controller = GetComponent<UnitController>();
    }

    private void Start()
    {
        stateMachine = GetComponent<UnitStateManager>();
    }

    private void Update()
    {
        animationHandler.SetPatrolBlend(agent.velocity.magnitude);
    }

    public void Init(UnitManager manager,InputHandler input)
    {
        unitManager = manager;
        inputHandler = input;
    }
}