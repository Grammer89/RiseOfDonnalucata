using System.Collections.Generic;
using UnityEngine;


public class GameState : GenericSingleton<GameState>
{
    [SerializeField] private InventoryState _inventoryState = new InventoryState();
    [SerializeField] private List<GameObject> _playerActive = new List<GameObject>();
    [SerializeField] private GameObject _actualMissionUI;
    [SerializeField] private int _numberOfMission = 1;

    public int NumberOfMission => _numberOfMission;
    public InventoryState Inventory => _inventoryState;
    public List<GameObject> PlayerActive => _playerActive;

    private List<SO_Quest> _listQuest = new List<SO_Quest>();
    private List<SO_Quest> _listQuestCompleted = new List<SO_Quest>();
    private const string _questNamePrefix = "Quests/";
    private bool _openItemShop = false;

    public bool OpenItemShop
    {
        get { return _openItemShop; }
        set { _openItemShop = value; }
    }
    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;
    }

    public void ModifyQuest(string quest, string nameQuest)
    {
        UI_ActualMission actualMissionUi = _actualMissionUI.GetComponentInChildren<UI_ActualMission>();
        int index = 0;

        string questName = _questNamePrefix + nameQuest;
        SO_Quest actualQuest = (SO_Quest)Resources.Load(questName);
        _listQuest.Add(actualQuest);

        foreach (SO_Quest singleQuest in _listQuest)
        {

            if (singleQuest.NameQuest() != nameQuest)
            {
                index += 1;
                continue;
            }
            Debug.Log("Mostro la nuova missione");
            _listQuest[index].StateQuest = StateMission.ToStart;
            actualMissionUi.ModifyMissionUI(singleQuest.DescriptionForUi());

        }
    }

    public void CompletedQuest()
    {
        if (_listQuest.Count == 0 ) return;
        SO_Quest completedQuest = _listQuest[0];
        completedQuest.StateQuest = StateMission.Completed;
        _listQuest.Remove(_listQuest[0]);
        _listQuestCompleted.Add(completedQuest);

        SO_GenericItem rewardItem = completedQuest.Reward();

        UI_ActualMission actualMissionUi = _actualMissionUI.GetComponentInChildren<UI_ActualMission>();
        actualMissionUi.ModifyMissionUI("Missione completata. Hai ottenuto : " + rewardItem.NameItem);

        Inventory.AddItem(rewardItem, 1);

        _numberOfMission += 1;

    }


}

