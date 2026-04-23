using UnityEngine;
public enum HeroType
{
    Paladin = 0,
    Warlock = 1,
    Ranger = 2,
    Alchimist = 3,

    Enemy = 99
}
[CreateAssetMenu(fileName = "New Creature", menuName = "Data/Creature")]
public class SO_Creatura : ScriptableObject
{
    [Header("Info Creature")]
    [SerializeField] private string _name;
    [SerializeField] private HeroType _creatureType;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _frz;
    [SerializeField] private int _res;
    [SerializeField] private int _vel;
    [SerializeField] private SO_GenericItem[] _rewards;
    

    public string Name => _name;
    public HeroType CreatureType => _creatureType;
    public int HpMax => _maxHp;
    public int Frz => _frz;
    public int Res => _res;
    public int Vel => _vel;

    public SO_GenericItem[] Rewards => _rewards;
  
}
