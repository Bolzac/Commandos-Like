using System;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Unit unit;


    private Collider[] _results = new Collider[4];
    private int _size;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public void VisualizeSelected(bool isSelected)
    {
        unit.model.selection.SetActive(isSelected);
    }

    public void Move(Vector3 destination)
    {
        unit.agent.SetDestination(destination);
    }

    public IEnumerator Interaction(IInteraction interaction)
    {
        Move(interaction.transform.position);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => interaction.distanceThreshold >= unit.agent.remainingDistance);
        unit.agent.ResetPath();
        if(unit.model.isCrouching) StandUp();
        DetectInteractionAnim(interaction);
        interaction.Interaction();
    }

    private void DetectInteractionAnim(IInteraction interaction)
    {
        switch (interaction.tag)
        {
            case "Enemy" :
                //unit.animationHandler.PlayTargetAnim("Stealth Assassination",0.25f);
                break;
            default:
                break;
        }
    }

    public void Crouch()
    {
        unit.model.isRunning = false;
        unit.model.isCrouching = true;
    }

    public void StandUp()
    {
        unit.model.isCrouching = false;
    }

    public void CreateNoise()
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