using UnityEngine;

public class StatisticPlayer : MonoBehaviour
{
    [SerializeField] private SO_Creatura _statisticHero;
    [SerializeField] private SO_HeroSpecialAction _statisticHeroSpecialAction;

    public SO_Creatura StatisticHero { get { return _statisticHero; } set { _statisticHero = value; } }
    public SO_HeroSpecialAction StatisticHeroSpecialAction
    {
        get
        { return _statisticHeroSpecialAction; }
        set
        { _statisticHeroSpecialAction = value; }
    }

}
