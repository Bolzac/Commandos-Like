using UnityEngine;
using UnityEngine.AI;

public class MemberController : MonoBehaviour
{
    public Member member;

    #region Commands

    public Interaction toInteraction;
    public MoveTo moveTo;
    public Crouch toCrouch;
    public StandUp toStandUp;

    #endregion
    
    private int _size;

    private void Awake()
    {
        member = GetComponent<Member>();
        toInteraction = new Interaction(member);
        moveTo = new MoveTo(member);
        toCrouch = new Crouch(member);
        toStandUp = new StandUp(member);
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

    public void Move(Vector3 destination, Interactable interaction = null)
    {
        member.commandManager.StopQueue();
        if (interaction == null) moveTo.destination = destination;
        else moveTo.destination = destination + interaction.interactionDistance * interaction.transform.forward;
        member.commandManager.AddCommand(moveTo);
    }

    public void Interaction(Interactable interaction)
    {
        toInteraction.interactable = interaction;
        member.commandManager.AddCommand(toInteraction);
    }

    public void StartInteraction(Interactable interaction)
    {
        Move(interaction.transform.position,interaction);
        member.commandManager.AddCommand(toStandUp);
        Interaction(interaction);
    }

    public void StartSkill()
    {
        
    }

    public void EnableLinkEvents()
    {
        member.agentLinkMover.onLinkStarts += OnLinkStarts;
        member.agentLinkMover.onLinkEnds += OnLinkEnds;
    }

    private void OnLinkEnds(OffMeshLink offMeshLink)
    {
        member.animationHandler.SetActionOff();
        member.stateManager.SetState(typeof(IdleState));
    }

    private void OnLinkStarts(OffMeshLink offMeshLink)
    {
        switch (offMeshLink.area)
        {
            case 2:
            {
                member.stateManager.SetState(typeof(JumpState));       
                break;
            }
            case 3:
            {
                member.stateManager.SetState(typeof(ClimbState));
                break;
            }
        }
    }
}