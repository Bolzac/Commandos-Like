using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class CharacterPanelHandler
{
    public GameObject characterPanel;

    public void InitPanel(Unit[] units)
    {
        ShowPanel();
        for (var i = 0; i < units.Length; i++)
        {
            var child = characterPanel.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<Image>().sprite = units[i].model.info.portrait;
        }
    }

    public void HidePanel()
    {
        characterPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        characterPanel.SetActive(true);
    }
}
