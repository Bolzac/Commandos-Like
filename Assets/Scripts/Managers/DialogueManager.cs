using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject[] choices;
    public Image unitSprite;
    
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    private List<Choice> currentChoices;
    private Unit speakerUnit;
    public bool DialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentChoices = new List<Choice>();
        DialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        for (var i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void StartDialogue(TextAsset dialogue, Unit unit)
    {
        UIManager.instance.skillPanelHandler.HidePanel();
        speakerUnit = unit;
        currentStory = new Story(dialogue.text);
        DialogueIsPlaying = true;
        dialogueBox.SetActive(true);
        unitSprite.sprite = speakerUnit.model.info.portrait;
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
        else
        {
            StartCoroutine(EndDialogue());
        }
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
        UIManager.instance.skillPanelHandler.ShowPanel();
        speakerUnit.model.isSpeaking = false;
        dialogueBox.SetActive(false);
        sentenceText.text = "";
        yield return new WaitForSeconds(0.1f);
        DialogueIsPlaying = false;
    }
}