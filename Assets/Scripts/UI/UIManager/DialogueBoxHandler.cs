using System;
using UnityEngine;

[Serializable]
public class DialogueBoxHandler
{
    [SerializeField] private GameObject dialoguePanel;

    public void ShowPanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void HidePanel()
    {
        dialoguePanel.SetActive(false);
    }
}