using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : IInteraction
{
    public Animation anim;

    public string doorOpeningAnim;
    public string doorClosingAnim;

    public bool isOpen;
    public bool isLocked;

    private void Awake()
    {
        anim = transform.parent.GetComponent<Animation>();
    }

    public override void Interaction()
    {
        PlayAccordingAnimations();
    }

    private void PlayAccordingAnimations()
    {
        if(isLocked) return;
        anim.CrossFade(isOpen ? doorClosingAnim : doorOpeningAnim, 0.25f);
        isOpen = !isOpen;
    }
}