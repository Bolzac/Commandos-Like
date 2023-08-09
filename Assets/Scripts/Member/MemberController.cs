using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MemberController : MonoBehaviour
{
    [FormerlySerializedAs("unit")] public Member member;

    private Collider[] _results = new Collider[4];
    private int _size;
    private readonly int _crouch = Animator.StringToHash("Crouch");

    public void VisualizeSelected(bool isSelected)
    {
        member.model.selection.SetActive(isSelected);
    }

    public void Move(Vector3 destination)
    {
        member.agent.SetDestination(destination);
    }

    public IEnumerator Interaction(IInteraction interaction)
    {
        yield return null;
        if (interaction.TryGetComponent(out NavMeshAgent agent) && agent.velocity.magnitude > 0)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                Move(interaction.transform.position);
                if(interaction.distanceThreshold >= member.agent.remainingDistance) break;
            }
        }
        else
        {
            Move(interaction.transform.position);
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => interaction.distanceThreshold >= member.agent.remainingDistance);   
        }
        member.agent.ResetPath();
        if(member.model.isCrouching) StandUp();
        member.model.isInteractedWithSomething = true;
        interaction.Interaction(member);
    }

    public void Crouch()
    {
        member.animationHandler.animator.SetBool(_crouch,true);
        member.model.isRunning = false;
        member.model.isCrouching = true;
    }

    public void StandUp()
    {
        member.animationHandler.animator.SetBool(_crouch,false);
        member.model.isCrouching = false;
    }

    public void CreateNoise()
    {
        _size = Physics.OverlapSphereNonAlloc(transform.position, member.model.runningNoiseRadius, _results
            /*Parent.Instance.team.teamModel.enemyLayer*/);
        if (_size > 0)
        {
            member.model.soundSource.position = transform.position;
            for (var i = 0; i < _size; i++)
            {
                _results[i].GetComponent<EnemyController>().ReactSound(member.model.soundSource);
            }
        }
    }

    public void SetReadySkill(int index)
    {
        member.model.readySkill = member.model.info.skills[index];
        member.stateManager.SetState(typeof(SkillReadyState));
    }

    public void DisableSkill()
    {
        member.model.readySkill = null;
        member.stateManager.SetState(typeof(IdleState));
    }

    public void SetFalseIsInteracted()
    {
        member.model.isInteractedWithSomething = false;
    }
}