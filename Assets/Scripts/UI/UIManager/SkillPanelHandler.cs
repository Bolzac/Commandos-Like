using System;
using UnityEngine;

[Serializable]
public class SkillPanelHandler
{
    public GameObject skillsPanel;

    public Transform skills;
    public Transform inventory;

    public void AddItemToInventory()
    {
        
    }

    public void ShowPanel()
    {
        //skillsPanel.SetActive(true);
    }

    public void HidePanel()
    {
        //skillsPanel.SetActive(false);
    }
}
