using System;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Unit unit;


    private Collider[] _results = new Collider[4];
    private int _size;
    private readonly int _crouchParam = Animator.StringToHash("Crouch");

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if(unit.model.isRunning) CreateNoise();
    }

    public void VisualizeSelected(bool isSelected)
    {
        unit.model.selection.SetActive(isSelected);
    }

    public void Move(Vector3 destination)
    {
        if (unit.model.isRunning) unit.agent.speed = unit.model.runningSpeed;
        else unit.agent.speed = unit.model.isCrouching ? unit.model.crouchSpeed : unit.model.walkingSpeed;
        unit.agent.SetDestination(destination);
    }

    public IEnumerator Interaction(IInteraction interaction)
    {
        Debug.Log("a");
        Move(interaction.animationTransform.position);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil((() => !unit.agent.hasPath));
        transform.forward = interaction.animationTransform.forward;
        if(unit.model.isCrouching) StandUp();
        interaction.Interaction();
    }

    public void Crouch()
    {
        unit.animationHandler.animator.SetBool(_crouchParam,true);
        unit.agent.speed = unit.model.crouchSpeed;
        unit.model.isCrouching = true;
    }

    public void StandUp()
    {
        unit.animationHandler.animator.SetBool(_crouchParam,false);
        unit.agent.speed = unit.model.walkingSpeed;
        unit.model.isCrouching = false;
    }

    private void CreateNoise()
    {
        _size = Physics.OverlapSphereNonAlloc(transform.position, unit.model.runningNoiseRadius, _results,
            unit.unitManager.teamModel.enemyLayer);
        if (_size > 0)
        {
            unit.model.soundSource.position = transform.position;
            for (var i = 0; i < _size; i++)
            {
                _results[i].GetComponent<EnemyController>().ReactSound(unit.model.soundSource);
            }
        }
    }
}