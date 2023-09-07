using System;
using UnityEngine;

public class Door : Interactable, IDataPersistence
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

    public void LoadData(GameData data)
    {
        data.interactions.TryGetValue(id, out isOpen);
        anim.CrossFade(!isOpen ? doorClosingAnim : doorOpeningAnim, 0);
    }

    public void SaveData(ref GameData data)
    {
        if (data.interactions.ContainsKey(id))
        {
            data.interactions.Remove(id);
        }
        data.interactions.Add(id,isOpen);
    }
}