
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CalculateDamages;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ActionType
{
    SimpleAttack = 0,
    Defense = 1,
    SpecialAction = 2,
    UseObject = 3

}
public class BattleManager : GenericSingleton<BattleManager>
{
    [Header("UI Battle Hero")]
    [SerializeField] private GameObject _uiBattleHero;
    [SerializeField] private GameObject _heroesUI;
    [Header("UI Name HP MP")]
    [SerializeField] private RectTransform _uiContentNameHpMp;
    [SerializeField] private GameObject _UInameHpMp;
    [Header("UI Hero does action")]
    [SerializeField] private TextMeshProUGUI _uiNameHeroDoesAction;
    [Header("UI Battle Hero/ Special Action & Object")]
    [SerializeField] private RectTransform _uiContent;
    [SerializeField] private GameObject _uiListObject;
    [SerializeField] private GameObject _uiSpecialAction;
    [SerializeField] private GameObject _uiObjectToUse;
    [Header("UI Action Button")]
    [SerializeField] private GameObject _uiActionButton;
    [Header("UI Target System Link")]
    [SerializeField] private UnityEvent _ueCallSystemTarget;
    [SerializeField] private GameObject _uiTargetSystem;
    [Header("Turn Battle")]
    [SerializeField] private List<OrderBattle> _orderBattles = new List<OrderBattle>();
    [Header("Heroes To Load")]
    [SerializeField] private GameObject[] _heroesActive; // Li recupero dalla cartella Resources Heroes
    [Header("Position Heroes")]
    [SerializeField] private Transform _startPositionHeroes;
    [SerializeField] private Transform _endPositionHeroes;
    [Header("Enemy to Load")]
    [SerializeField] private GameObject[] _enemies; // Li recupero dalla cartella Resources Heroes
    [Header("Position Enemies")]
    [SerializeField] private Transform _startPositionEnemies;
    [SerializeField] private Transform _endPositionEnemies;

    private string _folderEnemies = "Characters/Enemies/Mission2";
    private string _folderSpriteEnemies = "/Sprite";

    //Sprite ForTarget
    private Sprite[] _iconEnemies;

    private OrderBattle _heroDoAction = new OrderBattle();
    private string _targetName;
    private bool _nextAttack;
    private bool _calculateAttack;
    private ActionType _actionType;
    private ItemInstance _item;
    private SpecialActionWithSpendMp _specialActionHero;
    private List<GameObject> _warrior = new List<GameObject>();

    private int _numberEnemyDefeated;
    private int _numberHeroesDefeated;

    private bool _cameBack;
    public List<OrderBattle> OrderBattle => _orderBattles;
    public string TargetName { get { return _targetName; } set { _targetName = value; } }
    public bool NextAttack { get { return _nextAttack; } set { _nextAttack = value; } }
    public bool CalculateAttack { get { return _calculateAttack; } set { _calculateAttack = value; } }

    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;

        _uiBattleHero.SetActive(false);
        _uiListObject.SetActive(false);

        _heroesActive = Resources.LoadAll<GameObject>("Characters/Heroes");

        //_folderEnemies += GameState.Instance.NumberOfMission;
        _folderSpriteEnemies = _folderEnemies + _folderSpriteEnemies;
        Debug.Log("Sto caricando  i nemici della missione" + _folderEnemies);

        _enemies = Resources.LoadAll<GameObject>(_folderEnemies);
        _iconEnemies = new Sprite[_enemies.Length];
        _iconEnemies = Resources.LoadAll<Sprite>(_folderSpriteEnemies);
        Debug.Log("Il numero di immagini per i nemici sono: " + _iconEnemies.Length);
        SetupBattle();
    }

    private void Start()
    {

        StartCoroutine(Battle());
    }

    private void Update()
    {
        if (_cameBack)
        {
            _cameBack = false;
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void SetupBattle()
    {
        MenageSpawning();

        MenageOrderToAttack();

        SetNameHpMpHeroUi();
    }

    private void MenageSpawning()
    {
        SpawningPrefab(_heroesActive, _startPositionHeroes.position, _endPositionHeroes.position);
        SpawningPrefab(_enemies, _startPositionEnemies.position, _endPositionEnemies.position);
    }

    private void SpawningPrefab(GameObject[] creature, Vector3 startPosition, Vector3 endPosition)
    {
        float positionSegmentHeroes = (Mathf.Abs(startPosition.x) + Mathf.Abs(endPosition.x)) / (creature.Length + 1);

        for (int i = 0; i < creature.Length; i++)
        {

            GameObject prefabCreature = Instantiate(creature[i]);
            Vector3 positionCreature = new Vector3(startPosition.x + positionSegmentHeroes * (i + 1), 1, startPosition.z);
            prefabCreature.transform.position = positionCreature;

            DetermineIntiateToAttack(prefabCreature, i);
            _warrior.Add(prefabCreature);
        }
    }
    public void DetermineIntiateToAttack(GameObject creature, int index)
    {
        SO_Creatura SOcreature = new SO_Creatura();
        SO_HeroSpecialAction specialAction = new SO_HeroSpecialAction();
        StatisticPlayer statisticPlayer = creature.GetComponent<StatisticPlayer>();
        if (statisticPlayer != null)
        {
            SOcreature = statisticPlayer.StatisticHero;
            specialAction = statisticPlayer.StatisticHeroSpecialAction;
        }
        else
        {
            LifeController lifeController = creature.GetComponent<LifeController>();
            SOcreature = lifeController.Creature;
        }

        OrderBattle orderBattles = new OrderBattle();
        orderBattles.Creature = SOcreature;
        orderBattles.NameCreature = orderBattles.Creature.Name;
        orderBattles.NameGameObject = creature.name;
        if (orderBattles.Creature.CreatureType == HeroType.Enemy)
        {
            orderBattles.NameCreature += index.ToString();
            for (int i = 0; i < _iconEnemies.Length; i++)
            {
                if (orderBattles.NameGameObject.Contains(_iconEnemies[i].name))
                {
                    orderBattles.Icon = _iconEnemies[i];
                }
            }
        }
        else
        {
            orderBattles.Icon = statisticPlayer.Icon;

        }

        orderBattles.IniziativaRole = Random.Range(1, 21) + SOcreature.Vel;
        orderBattles.SpecialAction = specialAction;
        orderBattles.ActualHP = SOcreature.HpMax;
        orderBattles.ActualMP = SOcreature.MaxMp;

        _orderBattles.Add(orderBattles);

    }

    public void MenageOrderToAttack()
    {

        List<OrderBattle> orderBattle = _orderBattles.OrderByDescending(o => o.IniziativaRole).ToList();
        for (int i = 0; i < orderBattle.Count; i++)
        {
            _orderBattles[i] = orderBattle[i];
        }
    }

    public void SetNameHpMpHeroUi()
    {
        for (int i = 0; i < _orderBattles.Count; i++)
        {
            if (_orderBattles[i].Creature.CreatureType == HeroType.Enemy) continue;

            GameObject _UInameHpMpPrefab = Instantiate(_UInameHpMp, _uiContentNameHpMp);
            SingleTarget singleTarget = _UInameHpMpPrefab.GetComponent<SingleTarget>();
            if (singleTarget != null)
            {
                singleTarget.SetName(_orderBattles[i].NameCreature);
                singleTarget.SetHp(_orderBattles[i].ActualHP, _orderBattles[i].Creature.HpMax);
                singleTarget.SetMp(_orderBattles[i].ActualMP, _orderBattles[i].Creature.MaxMp);
            }
        }
    }
    public IEnumerator Battle()
    {
        _uiBattleHero.SetActive(true);

        WaitForSeconds wfs = new WaitForSeconds(0.5f);
        while (true)
        {
            foreach (var warrior in _orderBattles)
            {

                if (warrior.ActualHP <= 0)
                {
                    //Disattivo l'enemy dalla scena
                    if (warrior.Creature.CreatureType == HeroType.Enemy)
                    {
                        for (int j = 0; j < _warrior.Count; j++)
                        {
                            if (warrior.NameGameObject == _warrior[j].name
                               && _warrior[j].activeSelf)
                            {
                                _warrior[j].SetActive(false);
                            }
                        }
                        continue;
                    }
            }
                Debug.Log("Fa l'azione: " + warrior.NameCreature);
                if (warrior.Creature.CreatureType == HeroType.Enemy)
                {

                    if (_uiBattleHero.activeSelf)
                    {
                        _heroesUI.SetActive(false);
                        AttackEnemy(warrior);

                    }
                    //Gestione interna degli attacchi dei nemici
                    yield return wfs;
                }
                else
                {
                    RemoveButton();
                    _heroDoAction = warrior; //Serve per capire chi sta eseguendo l'azione
                    _heroesUI.SetActive(true);
                    SetNameHeroDoesAction(_heroDoAction);
                    yield return new WaitUntil(() => _nextAttack == true);
                    _heroesUI.SetActive(false);
                    _nextAttack = false;
                    yield return wfs;
                }

                if (CheckExitBattle())
                {
                    _cameBack = true;
                    break;
                }
            }
            if (CheckExitBattle())
            {
                _cameBack = true;
                break;
            }
        }
    }


    public void OnClickAttack()
    {
        if (_uiTargetSystem.activeSelf) return;

        _actionType = ActionType.SimpleAttack;
        RemoveButton();
        StartCoroutine(ActionToTarget());
    }

    public void OnClickSpecialAttackMenu()
    {
        if (_uiTargetSystem.activeSelf) return;
        RemoveButton();
        _uiListObject.SetActive(true);
        for (int i = 0; i < _heroDoAction.SpecialAction.SpecialActionHero.Length; i++)
        {
            GameObject specialAction = Instantiate(_uiSpecialAction, _uiContent);
            TextMeshProUGUI[] texts = specialAction.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts != null)
            {
                texts[0].SetText(_heroDoAction.SpecialAction.SpecialActionHero[i]._specialActionHero.ToString());
                texts[1].SetText(_heroDoAction.SpecialAction.SpecialActionHero[i]._mpToSpend.ToString() + " MP");
            }
            Button button = specialAction.GetComponentInChildren<Button>();
            if (button != null)
            {
                if (_heroDoAction.ActualMP == 0)
                {
                    button.interactable = false;
                    continue;
                }
                SpecialActionWithSpendMp specialActionHeroWithHp = _heroDoAction.SpecialAction.SpecialActionHero[i];
                button.onClick.AddListener(delegate { OnClickSpecialAction(specialActionHeroWithHp); });
            }
        }
    }


    public void OnClickDefense()
    {
        if (_uiTargetSystem.activeSelf) return;
        _actionType = ActionType.Defense;
        SetDifense();
        _nextAttack = true;
    }


    public void OnClickObjectToUseMenu()
    {
        if (_uiTargetSystem.activeSelf) return;

        RemoveButton();
        _uiListObject.SetActive(true);
        for (int i = 0; i < GameState.Instance.Inventory.Items.Count; i++)
        {
            GameObject objectToUse = Instantiate(_uiObjectToUse, _uiContent);
            TextMeshProUGUI[] texts = objectToUse.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts != null)
            {
                ItemInstance item = GameState.Instance.Inventory.GetItem(i);
                texts[0].SetText(item.Data.NameItem);
                texts[1].SetText("x " + item.Amount.ToString());
            }
            Button button = objectToUse.GetComponentInChildren<Button>();
            if (button != null)
            {
                if (GameState.Instance.Inventory.Items[i].Amount == 0)
                {
                    button.interactable = false;
                    continue;
                }

                ItemInstance item = GameState.Instance.Inventory.GetItem(i);
                button.onClick.AddListener(delegate { OnClickObjectToUse(item); });
            }
        }
    }

    public void OnClickSpecialAction(SpecialActionWithSpendMp specialActionWithMp)
    {
        Debug.Log("Scegliamo chi attaccare con l'attacco speciale");
        if (_uiTargetSystem.activeSelf) return;
        _actionType = ActionType.SpecialAction;
        _specialActionHero = specialActionWithMp;
        StartCoroutine(ActionToTarget());
    }

    public void OnClickObjectToUse(ItemInstance item)
    {
        if (_uiTargetSystem.activeSelf) return;
        _actionType = ActionType.UseObject;
        _item = item;
        StartCoroutine(ActionToTarget());
    }

    public void RemoveButton()
    {
        Button[] button = _uiContent?.GetComponentsInChildren<Button>();
        if (button != null)
        {
            for (int i = 0; i < button.Length; i++)
            {
                DestroyImmediate(button[i].gameObject);
            }
        }
    }

    public void CallSystemTargeting()
    {
        _ueCallSystemTarget?.Invoke();
    }

    public IEnumerator ActionToTarget()
    {
        Debug.Log("Aspetto fino a che l'utente non ha scelto il target");
        CallSystemTargeting();
        yield return new WaitUntil(() => _calculateAttack);
        _calculateAttack = false;
        CallMethodForDamage(_actionType, _heroDoAction, _targetName, _specialActionHero, _item);
        StopCoroutine(ActionToTarget());
        _nextAttack = true;
    }

    public void CallMethodForDamage(ActionType action,
                                   OrderBattle fromTarget,
                                   string nameTarget,
                                   SpecialActionWithSpendMp specialActionWithMp,
                                   ItemInstance item)
    {
        OrderBattle toTarget = new OrderBattle();
        for (int i = 0; i < _orderBattles.Count; i++)
        {
            if (_orderBattles[i].NameCreature == nameTarget)
            {
                toTarget = (_orderBattles[i]);
                break;
            }
        }
        int damage = CalculateDamage.Calculate(action, fromTarget, toTarget, specialActionWithMp, item);
        Debug.Log(toTarget.NameCreature + " ha subito un attacco di tipo: " + action + " da " + fromTarget.NameCreature + " di " + damage + " HP");


        SetDamage(action, damage, toTarget, specialActionWithMp, item);
        SetMpToSpend(action, fromTarget, specialActionWithMp);

        Debug.Log(toTarget.NameCreature + " gli restano: " + toTarget.ActualHP + " HP");
        Debug.Log(fromTarget.NameCreature + "Ha speso " + specialActionWithMp._mpToSpend + "MP");
        Debug.Log("Gliene restano: " + fromTarget.ActualMP);
        UI_SetHPMP(fromTarget, toTarget, action);
        SetDamageUI(toTarget, damage);
    }

    public void SetDamage(ActionType action, int damage, OrderBattle toTarget, SpecialActionWithSpendMp specialActionWithMp, ItemInstance item)
    {
        if (MenageModifyAttributionStatistic(action, damage, toTarget, specialActionWithMp)) return;

        if (damage == 0) return;

        switch (action)
        {
            case (ActionType.SimpleAttack):
                {
                    toTarget.ActualHP = Mathf.Clamp(toTarget.ActualHP - damage, 0, toTarget.Creature.HpMax);
                    break;
                }
            case (ActionType.Defense):
                {
                    toTarget.Creature.Res += 5;
                    break;
                }

            case (ActionType.SpecialAction):
                {
                    toTarget.ActualHP = Mathf.Clamp(toTarget.ActualHP - damage, 0, toTarget.Creature.HpMax);
                    break;
                }
            case (ActionType.UseObject):
                {
                    toTarget.ActualHP = Mathf.Clamp(toTarget.ActualHP + damage, 0, toTarget.Creature.HpMax);
                    item.Amount -= 1;
                    break;
                }
        }

        Debug.Log(toTarget.Creature.CreatureType + " " + toTarget.ActualHP);
        if (toTarget.Creature.CreatureType == HeroType.Enemy && toTarget.ActualHP <= 0)
        {
            _numberEnemyDefeated += 1;
            Debug.Log("Numero di nemici sconfitti al momento: " + _numberEnemyDefeated);
        }
        else if (((toTarget.Creature.CreatureType == HeroType.Alchimist) ||
                  (toTarget.Creature.CreatureType == HeroType.Paladin) ||
                  (toTarget.Creature.CreatureType == HeroType.Ranger) ||
                  (toTarget.Creature.CreatureType == HeroType.Warlock)) &&
                    toTarget.ActualHP <= 0)
        {
            _numberHeroesDefeated += 1;
        }


    }

    public void SetMpToSpend(ActionType action, OrderBattle fromTarget, SpecialActionWithSpendMp specialActionWithMp)
    {
        if (action != ActionType.SpecialAction) return;

        fromTarget.ActualMP = Mathf.Clamp(fromTarget.ActualMP - specialActionWithMp._mpToSpend, 0, fromTarget.Creature.MaxMp);
    }
    public bool MenageModifyAttributionStatistic(ActionType action, int damage, OrderBattle toTarget, SpecialActionWithSpendMp specialActionWithMp)
    {
        if (specialActionWithMp._specialActionHero == SpecialActionHero.Cure)
        {
            toTarget.ActualHP = Mathf.Clamp(toTarget.ActualHP + damage, 0, toTarget.Creature.HpMax);
            return true;
        }
        return false;
    }

    public bool CheckExitBattle()
    {
        return (_numberEnemyDefeated == _enemies.Length || _numberHeroesDefeated == _heroesActive.Length);

    }

    public void SetDifense()
    {
        _heroDoAction.Creature.Res += 5;
    }

    public void UI_SetHPMP(OrderBattle fromTarget, OrderBattle toTarget, ActionType actionType)
    {
        RectTransform[] singleHero = _uiContentNameHpMp.GetComponentsInChildren<RectTransform>();

        if (singleHero == null) return;


        //Gestione Hero attacca nemico con azione speciale

        for (int i = 0; i < singleHero.Length; i++)
        {
            SingleTarget singleTarget = singleHero[i].GetComponent<SingleTarget>();
            if (singleTarget != null)
            {
                if (toTarget.NameCreature == singleTarget.GetNameText())
                {
                    singleTarget.SetHp(toTarget.ActualHP, toTarget.Creature.HpMax);
                }

                if (fromTarget.NameCreature == singleTarget.GetNameText())
                {
                    singleTarget.SetMp(fromTarget.ActualMP, fromTarget.Creature.MaxMp);
                }

            }

        }

    }

    private void SetNameHeroDoesAction(OrderBattle _heroDoAction)
    {
        _uiNameHeroDoesAction.SetText(_heroDoAction.NameCreature);
    }
    public void SetDamageUI(OrderBattle toTarget, int damage)
    {
        Debug.Log(toTarget.NameGameObject);
        for (int i = 0; i < _warrior.Count; i++)
        {

            if (_warrior[i].name != toTarget.NameGameObject) continue;

            UI_ChangeHitValue hitDamageUI = _warrior[i].GetComponent<UI_ChangeHitValue>();
            if (hitDamageUI != null)
            {
                Debug.Log("Setto nella UI" + damage + "per toTarget.NamgeGameObject");
                hitDamageUI.On_ChangeHit(damage);
            }
            break;
        }

    }

    public void AttackEnemy(OrderBattle enemy)
    {
        int indexHeroToAttack = Random.Range(1, _heroesActive.Count());
        int index = 0;
        _actionType = ActionType.SimpleAttack;
        //determino in modo random l'eroe da attaccare
        string nameTarget = "";
        for (int i = 0; i < _orderBattles.Count; i++)
        {
            if (_orderBattles[i].Creature.CreatureType != HeroType.Enemy)
            {
                index += 1;
                if (index == indexHeroToAttack)
                {
                    nameTarget = _orderBattles[i].NameCreature;
                    break;
                }
            }
        }
        _calculateAttack = false;
        CallMethodForDamage(_actionType, enemy, nameTarget, _specialActionHero, _item);
    }
}
