using UnityEngine;
using System;

[Serializable]
public struct SpecialActionWithSpendMp
{
    public SpecialActionHero _specialActionHero;
    public int _mpToSpend;
}
public enum SpecialActionHero
{
    //PALADIN
    Shield = 0,
    Cure = 1,


    //RANGER
    Scan = 20,
    FieryArrow = 21,


    //WARLOCK
    Fire = 40,
    Blizzard = 41,
    Thunder = 42

}
[CreateAssetMenu(fileName = "New Hero", menuName = "Data/Hero")]
public class SO_HeroSpecialAction : ScriptableObject
{
    [SerializeField] private HeroType _creatureType;
    [SerializeField] private SpecialActionWithSpendMp[] _specialActionHero;


    public HeroType CreatureType => _creatureType;
    public SpecialActionWithSpendMp[] SpecialActionHero => _specialActionHero;

}
