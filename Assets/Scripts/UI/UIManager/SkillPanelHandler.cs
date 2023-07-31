using System;
using UnityEngine;

[Serializable]
public class SkillPanelHandler
{
    public GameObject skillsPanel;
    public Transform skillBar;

    public void SetSkills(Member newSelected)
    {
        for (var i = 0; i < newSelected.model.info.skills.Length; i++)
        {
            var child = skillBar.GetChild(i);
            child.GetComponent<UISkill>().SetSkillBlock(newSelected.model.info.skills[i]);
        }
    }

    public void ShowPanel()
    {
        skillsPanel.SetActive(true);
    }

    public void HidePanel()
    {
        skillsPanel.SetActive(false);
    }
}
