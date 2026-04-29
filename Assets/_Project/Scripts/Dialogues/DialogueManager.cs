using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : GenericSingleton<DialogueManager>
{
    [Header("Params")]
    [SerializeField] private float _typingSpeed = 0.04f;

    [Header("Load Globals Json")]
    [SerializeField] private TextAsset _loadGlobalJson;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialogueCanvas;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] _choices;

    private TextMeshProUGUI[] _choicesText;

    private Story _currentStory;

    private bool _dialogueIsPlaying;

    private DialogueVariables _dialogueVariables;

    private Coroutine displayLineCoroutine; //Serve per evitare sovrapposizione di testi

    public bool DialogueIsPlaying { get => _dialogueIsPlaying; }

    public DialogueVariables GetDialogueVariables() => _dialogueVariables;
    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;

        _dialogueVariables = new DialogueVariables(_loadGlobalJson);
    }

    private void Start()
    {
        _dialogueIsPlaying = false;
        _dialogueCanvas.SetActive(false);

        //get all of the choices text
        _choicesText = new TextMeshProUGUI[_choices.Length];
        int index = 0;
        foreach (GameObject choice in _choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!_dialogueIsPlaying)
        {
            return;
        }
        //handle continuing to the next line in the dialogue when submit is pressed
        if (_currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }
    public void EnterDialogueMode(TextAsset inkJson)
    {
        _currentStory = new Story(inkJson.text);
        _dialogueIsPlaying = true;
        _dialogueCanvas.SetActive(true);

        _dialogueVariables.StartListening(_currentStory);
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        _dialogueVariables.StopListening(_currentStory);

        _dialogueIsPlaying = false;
        _dialogueCanvas.SetActive(false);
        _dialogueText.SetText(string.Empty);

    }

    private IEnumerator DisplayLine(string line)
    {
        _dialogueText.SetText("");
        WaitForSeconds waitForSeconds = new WaitForSeconds(_typingSpeed);
        //Mostriamo una lettera alla volta
        foreach (char letter in line.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return waitForSeconds;

        }
    }
    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            ////////case1 :
            ////////Set Text for the current dialogue line
            //////_dialogueText.text = _currentStory.Continue();
            //case2:
            //Set Text Using one word for time
            displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));
            // display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        //Acquisizione le scelte attuali del dialogo
        List<Choice> currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > _choices.Length)
        {
            Debug.LogError("Ci sono più scelte che la UI può supportare, numero di scelte date:"
                            + currentChoices.Count);
        }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            _choices[index].gameObject.SetActive(true);
            _choicesText[index].SetText(choice.text);
            index++;
        }
        //disabilita i bottoni restanti presenti nella UI
        for (int i = index; i < _choices.Length; i++)
        {
            _choices[i].gameObject.SetActive(false);

        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        //Ci permette di selezionare i bottoni sulla UI....
        //L'event System vuole che puliamo prima, poi aspettiamo
        // 1 frame prima di settare l'attuale risposta
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        _dialogueVariables.Variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    //Metodo che viene chiaamto ogni volta che esco dall'applicazione
    public void OnApplicationQuit()
    {
        if (_dialogueVariables != null)
        {
            _dialogueVariables.SaveVariables();
        }
    }
}
