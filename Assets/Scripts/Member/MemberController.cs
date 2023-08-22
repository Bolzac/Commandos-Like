using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MemberController : MonoBehaviour
{
    public Member member;
    
    private Collider[] _results = new Collider[4];
    private int _size;
    private readonly int _crouch = Animator.StringToHash("Crouch");
    private Coroutine _interactionCoroutine;

    private void Awake()
    {
        member = GetComponent<Member>();
    }

    private void Start()
    {
        EnableLinkEvents();
    }

    public void VisualizeSelected(bool isSelected)
    {
        member.model.selectedVisual.SetActive(isSelected);
        member.SetClicked(isSelected);
        member.SetOutline(isSelected);
    }

    public void Move(Vector3 destination)
    {
        member.agent.SetDestination(destination);
    }

    public void StartInteraction(Interactable interaction)
    {
        _interactionCoroutine = StartCoroutine(HandleInteraction(interaction));
    }

    public void StopInteraction()
    {
        if(_interactionCoroutine != null) StopCoroutine(_interactionCoroutine);
    }

    public bool IsInteracting()
    {
        return _interactionCoroutine != null;
    }

    private IEnumerator HandleInteraction(Interactable interaction)
    {
        if (!member.animationHandler.IsActionDone()) yield break;
        yield return null;
        if (interaction.TryGetComponent(out NavMeshAgent agent) && agent.velocity.magnitude > 0)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                Move(interaction.transform.position);
                if(member.agent.pathEndPosition == member.transform.position) break;
            }
        }
        else
        {
            Move(interaction.transform.position);
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => member.agent.pathEndPosition == member.transform.position);
        }
        if(member.model.isCrouching) StandUp();
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

    public void EnableLinkEvents()
    {
        member.agentLinkMover.onLinkStarts += OnLinkStarts;
        member.agentLinkMover.onLinkEnds += OnLinkEnds;
    }

    private void OnLinkEnds(Vector3 lookPos)
    {
        member.stateManager.SetState(typeof(IdleState));
    }

    private void OnLinkStarts(Vector3 lookPos)
    {
        transform.LookAt(lookPos);
        member.stateManager.SetState(typeof(JumpState));
    }
}