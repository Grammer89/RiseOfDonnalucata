using System.Collections.Generic;
//using Ink.Parsed;

//using Ink.Parsed;
using Ink.Runtime;
using UnityEngine;


public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> Variables { get; private set; }

    private Story _globalVariablesStory;

    private const string _saveVariablesKey = "INK_VARIABLES";
    private const string _nameMissionId = "MissionId";
    private const string _missionCompleted = "MissionCompleted";
    private const string _openItemShop = "OpenItemShop";
    private const string _CanIfight = "CanIfight";
    private const string _comingSoon = "ComingSoon";
    public DialogueVariables(TextAsset loadGlobalJSON)
    {
        //create the story
        _globalVariablesStory = new Story(loadGlobalJSON.text);
        //if (PlayerPrefs.HasKey(_saveVariablesKey))
        //{
        //    string jsonState = PlayerPrefs.GetString(_saveVariablesKey);
        //    Debug.Log(jsonState);
        //    _globalVariablesStory.state.LoadJson(jsonState);
        //}

        //initialize the dictionary
        Variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in _globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            Variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable " + name + " = " + value);
        }

    }
    public bool GetBool(string variableName) => GetValue<bool>(variableName, false);

    public int GetInt(string variableName) => GetValue<int>(variableName, 0);

    public string GetString(string variableName) => GetValue<string>(variableName, " ");

    private T GetValue<T>(string variableName, T defaultValue)
    {
        if (Variables.ContainsKey(variableName))
        {
            Ink.Runtime.Value inkValue = Variables[variableName] as Ink.Runtime.Value;
            if (inkValue.valueObject is T casted)
            {
                return casted;
            }
            else
            {
                Debug.LogError($"The variable {variableName} is not of type {typeof(T)}");
            }
        }
        else
        {
            Debug.LogError("Non esiste una variabile di nome " + variableName);
        }
        return defaultValue;
    }

    public void SetBool(string variableName, bool value)
    {
        Ink.Runtime.BoolValue boolean = GetOrCreateVariableValue<Ink.Runtime.BoolValue>(variableName);
        boolean.value = value;
    }

    public void SetInt(string variableName, int value)
    {
        Ink.Runtime.IntValue integer = GetOrCreateVariableValue<Ink.Runtime.IntValue>(variableName);
        integer.value = value;
    }

    public void SetString(string variableName, string value)
    {
        Ink.Runtime.StringValue stringValue = GetOrCreateVariableValue<Ink.Runtime.StringValue>(variableName);
        stringValue.value = value;
    }
    private T GetOrCreateVariableValue<T>(string variableName) where T : Ink.Runtime.Value, new()
    {
        T variable;
        if (Variables.ContainsKey(variableName))
        {
            variable = Variables[variableName] as T;
        }
        else
        {
            variable = new T();
            Variables[variableName] = variable;
        }
        return variable;
    }

    public void SaveVariables()
    {
        if (_globalVariablesStory != null)
        {
            // Load the current state of all of our variables to the globals story
            VariablesToStory(_globalVariablesStory);
            //Eventualmente si può usare l'altro metodo(consigliato da Jacopo e Luca)
            PlayerPrefs.SetString(_saveVariablesKey, _globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story)
    {
        //assegnazione necessarie prima dell'azione del listener
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //gestione variabili che sono state inizializzate dal global ink file
        if (Variables.ContainsKey(name))
        {
            Debug.Log(name + " ha valore " + value);

            Variables.Remove(name);
            Variables.Add(name, value);


            if (name == _nameMissionId)
            {

                GameState.Instance.ModifyQuest(name, GetString(name));
            }
            else if (name == _missionCompleted)
            {
                GameState.Instance.CompletedQuest();
            }
            else if (name == _openItemShop)
            {
                Debug.Log("Setto la variabile name con: " + value);
                GameState.Instance.OpenItemShop = value;
            }
            else if (name == _CanIfight)
            {
                GameState.Instance.CanIfight = value;
            }
            else if (name == _comingSoon)
            {

                Debug.Log("Elimino tutte le variabili");
                DeleteAllPrefs();
                GameState.Instance.ComingSoon = value;
            }

        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in Variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }

    }

    public void DeleteAllPrefs()
    {
        PlayerPrefs.SetString(_saveVariablesKey, "");
        PlayerPrefs.SetString(_nameMissionId, "");
        PlayerPrefs.SetString(_missionCompleted, "");
        PlayerPrefs.SetString(_openItemShop, "");
        PlayerPrefs.SetString(_CanIfight, "");
        PlayerPrefs.SetString(_comingSoon, "");
        PlayerPrefs.Save();

        foreach (string name in _globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            Variables.Remove(name);
        }
    }
}
