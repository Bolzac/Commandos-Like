using System;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Unit unit;


    private Collider[] _results = new Collider[4];
    private int _size;

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
        if (interaction.isSpeakable) DialogueManager.instance.unitSprite.sprite = unit.model.info.portrait;
        unit.agent.ResetPath();
        if(unit.model.isCrouching) StandUp();
        DetectInteractionAnim(interaction);
        unit.model.isInteractedWithSomething = true;
        interaction.Interaction(unit);
    }

    private void DetectInteractionAnim(IInteraction interaction)
    {
        switch (interaction.tag)
        {
            case "Enemy" :
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

    public void SetReadySkill(int index)
    {
        unit.model.readySkill = unit.model.info.skills[index];
        unit.stateManager.SetState(typeof(SkillReadyState));
    }

    public void DisableSkill()
    {
        unit.model.readySkill = null;
        unit.stateManager.SetState(typeof(IdleState));
    }

    public void SetFalseIsInteracted()
    {
        unit.model.isInteractedWithSomething = false;
    }
}