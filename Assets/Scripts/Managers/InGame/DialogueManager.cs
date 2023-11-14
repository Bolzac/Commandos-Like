using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private Animator portraitAnimator;
    public GameObject[] choices;
    [SerializeField] private Image unitSprite;

    private TextMeshProUGUI[] choicesText;
    public Story currentStory;
    private List<Choice> currentChoices;
    public Member speakerMember;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    private void Awake()
    {
        foreach (var npcBase in FindObjectsOfType<NPCBase>())
        {
            npcBase.OnDialogue += StartDialogue;
        }
        
        currentChoices = new List<Choice>();
        choicesText = new TextMeshProUGUI[choices.Length];
        for (var i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void StartDialogue(TextAsset dialogue, Member member)
    {
        InGameManager.Instance.cameraController.FocusOnDialogue(member.model.follow);
        InGameManager.Instance.SetState(typeof(DialogueState));
        TeamManagement.Instance.SelectOneUnit(member.index);

        speakerMember = member;
        unitSprite.sprite = speakerMember.model.info.portrait;

        currentStory = new Story(dialogue.text);

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (currentStory.canContinue)
        {
            sentenceText.text = currentStory.Continue();
            if (sentenceText.text.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(EndDialogue());
                return;
            }
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else StartCoroutine(EndDialogue());
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tog couldn't be appropriately parsed:" + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    nameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled:" + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support.");
        }
        
        for (var i = 0; i < currentChoices.Count; i++)
        {
            choices[i].SetActive(true);
            choicesText[i].text = currentChoices[i].text;
        }

        for (int i = currentChoices.Count; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        DisplayNextSentence();
    }

    private IEnumerator EndDialogue()
    {
        InGameManager.Instance.SetState(typeof(PlayState));
        sentenceText.text = "";
        yield return new WaitForSeconds(0.1f);
    }
}