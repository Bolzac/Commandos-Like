using UnityEngine;
using UnityEngine.AI;

public class Enemy : IInteraction
{
    public Camera cam;
    public AnimationHandler animationHandler;
    public NavMeshAgent agent;
    public EnemyStateManager enemyStateMachine;
    public FieldOfView fov;
    public EnemyModel model;
    public EnemyController controller;
    public EnemyView view;

    private void Awake()
    {
        animationHandler = transform.GetChild(0).GetComponent<AnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        model = GetComponent<EnemyModel>();
        controller = GetComponent<EnemyController>();
        view = GetComponent<EnemyView>();
    }

    private void Start()
    {
        enemyStateMachine = GetComponent<EnemyStateManager>();
    }

    private void Update()
    {
        animationHandler.SetPatrolBlend(agent.velocity.magnitude);
    }

    public override void Interaction(Member member)
    {
        controller.Die(member);
    }
}