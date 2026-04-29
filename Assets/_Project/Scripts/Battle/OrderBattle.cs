using System;
using UnityEngine;
[Serializable]
public class OrderBattle
{
    [SerializeField] private SO_Creatura _creature;
    [SerializeField] private int _iniziativaRole;
    [SerializeField] private SO_HeroSpecialAction _specialAction;
    [SerializeField] private int _actualHP;
    [SerializeField] private int _actualMP;
    [SerializeField] private string _nameCreature;
    public SO_Creatura 
        Creature
    {
        get { return _creature; }
        set { _creature = value; }
    }

    public int IniziativaRole
    {
        get { return _iniziativaRole; }
        set { _iniziativaRole = value; }
    }

    public SO_HeroSpecialAction SpecialAction
    {
        get { return _specialAction; }
        set { _specialAction = value; }
    }

    public string NameCreature
    {
        get { return _nameCreature; }
        set { _nameCreature = value; }
    }

    public int ActualHP
    {
        get { return _actualHP; }
        set { _actualHP = value; }
    }

    public int ActualMP
    {
        get { return _actualMP; }
        set { _actualMP = value; }
    }

}
