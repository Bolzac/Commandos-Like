using System;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    public Animation anim;

    public string doorOpeningAnim;
    public string doorClosingAnim;

    public bool isOpen;
    public bool isLocked;

    protected override void Awake()
    {
        base.Awake();
        anim = transform.GetChild(0).GetComponent<Animation>();
    }

    public override void Interaction(Member member)
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