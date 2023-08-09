using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour, ISelectHandler
{
    protected Selectable targetButton;

    private void Awake()
    {
        targetButton = GetComponent<Selectable>();
    }

    public abstract void OnSelect(BaseEventData eventData);
}
