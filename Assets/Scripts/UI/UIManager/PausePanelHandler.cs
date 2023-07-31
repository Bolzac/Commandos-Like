using System;
using UnityEngine;

[Serializable]
public class PausePanelHandler
{
    [SerializeField] private GameObject pausePanel;

    public void ShowPanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePanel()
    {
        pausePanel.SetActive(false);
    }
}