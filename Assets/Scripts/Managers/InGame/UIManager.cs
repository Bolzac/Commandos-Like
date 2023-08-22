using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SelectionBoxHandler selectionBoxHandler;
    public CharacterPanelHandler characterPanelHandler;
    public SkillPanelHandler skillPanelHandler;
    public DialogueBoxHandler dialogueBoxHandler;
    public PausePanelHandler pausePanelHandler;

    private void Awake()
    {
        TeamManagement.Instance.OnMemberChanged += SetSkills;
    }

    private void Start()
    {
        InitCharacterPanel(TeamManagement.Instance.members);
    }

    private void InitCharacterPanel(Member[] members) { characterPanelHandler.InitPanel(members); }

    public void SetSkills(Member member) { skillPanelHandler.SetSkills(member); }

    public void StartDialogueUI() { dialogueBoxHandler.ShowPanel(); }

    public void ExitDialogueUI() { dialogueBoxHandler.HidePanel(); }

    public void StartPlayUI()
    {
        skillPanelHandler.ShowPanel();
        characterPanelHandler.ShowPanel();
    }

    public void ExitPlayUI()
    {
        skillPanelHandler.HidePanel();
        characterPanelHandler.HidePanel();
    }

    public void EnableMultipleSelectionUI(Vector2 a, Vector2 b) { selectionBoxHandler.CreateSelectionBox(a,b); }

    public void DisableMultipleSelectionUI() { selectionBoxHandler.DisableSelectionBox(); }

    public void StartPauseUI() { pausePanelHandler.ShowPanel(); }

    public void ExitPauseUI() { pausePanelHandler.HidePanel(); }
}
