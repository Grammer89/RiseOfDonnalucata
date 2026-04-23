using UnityEngine;

public enum StateMission 
{
   ToStart = 0 ,
   Started = 1 ,
   Completed = 2 
}
[CreateAssetMenu(fileName = "New Quest", menuName = "Data/Quest")]
public class SO_Quest : ScriptableObject
{
 
    [SerializeField] private string _nameQuest;
    [SerializeField] private StateMission _stateQuest;
    [SerializeField] private string _descriptionForUi;
    [SerializeField] private string _descriptionForMenu;
    [SerializeField] private SO_GenericItem _reward;


    public string NameQuest() => _nameQuest;
    public StateMission StateQuest{ get { return _stateQuest; } set { _stateQuest = value; } }
    public string DescriptionForUi() => _descriptionForUi;
    public string DescriptionForMenu() => _descriptionForMenu;
    public SO_GenericItem Reward() => _reward;
}
