using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineFlowController : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public List<TimelineAsset> timelineAssets;

    public bool startWithDialogue;

    public TextAsset dialogue;
    public Member member;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        if(startWithDialogue) StartDialogue();
    }

    public void StartDialogue()
    {
        InGameManager.Instance.dialogueManager.StartDialogue(dialogue,member);
        CutSceneFlow();
    }

    public void CloseChoices()
    {
        foreach (var choice in InGameManager.Instance.dialogueManager.choices) choice.SetActive(false);
    }

    public void OpenChoices()
    {
        for (var i = 0; i < InGameManager.Instance.dialogueManager.currentStory.currentChoices.Count; i++)
        {
            InGameManager.Instance.dialogueManager.choices[i].SetActive(true);
        }
    }

    public void CutSceneFlow()
    {
        InGameManager.Instance.dialogueManager.currentStory.ObserveVariable("timeline",(variableName, value) =>
        {
            playableDirector.playableAsset = timelineAssets[(int)value];
            playableDirector.Play();
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        member = other.GetComponent<Member>();
        StartDialogue();
    }
}