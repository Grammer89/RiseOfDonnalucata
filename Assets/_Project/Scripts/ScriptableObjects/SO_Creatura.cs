using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private int _maxMp;
    [SerializeField] private int _frz;
    [SerializeField] private int _res;
    [SerializeField] private int _mag;
    [SerializeField] private int _spr;
    [SerializeField] private int _vel;

    [SerializeField] private SO_GenericItem[] _rewards;
    [SerializeField] private Sprite _icon;


    public string Name { get { return _name; } set { _name = value; } }
    public HeroType CreatureType => _creatureType;
    public int HpMax => _maxHp;
    public int MaxMp => _maxMp;
    public int Frz => _frz;
    public int Res { get { return _res; } set { _res = value; } }
    public int Mag => _mag;
    public int Spr => _spr;
    public int Vel => _vel;
    public SO_GenericItem[] Rewards => _rewards;
    public Sprite Icon => _icon;

}
