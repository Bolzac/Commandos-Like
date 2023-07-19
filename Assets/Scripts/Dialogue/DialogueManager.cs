using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private GameObject[] choices;
    
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    private List<Choice> currentChoices;
    public bool DialogueIsPlaying { get; private set; }
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

    public void StartDialogue(string npcName, TextAsset dialogue)
    {
        nameText.text = npcName;
        currentStory = new Story(dialogue.text);
        DialogueIsPlaying = true;
        dialogueBox.SetActive(true);
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
        }
        else
        {
            StartCoroutine(EndDialogue());
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
        dialogueBox.SetActive(false);
        sentenceText.text = "";
        yield return new WaitForSeconds(0.1f);
        DialogueIsPlaying = false;
    }
}