using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : GenericSingleton<BattleManager>
{
    [SerializeField] List<GameObject> _heroesActive = new List<GameObject>();
    [SerializeField] List<GameObject> _heroesUI = new List<GameObject>();

    private bool _nextAttack;
    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;
        foreach (var heroesUI in _heroesUI)
        {
            heroesUI.SetActive(false);
        }
    }

    private void Start()
    {

        StartCoroutine(Battle());
;

    }

    public void DetermineOrderToAttack()
    {

    }

    public IEnumerator Battle()
    {
        foreach (var heroesUI in _heroesUI)
        {
            heroesUI.SetActive(true);
            yield return new WaitUntil(() => _nextAttack == true);
            heroesUI.SetActive(false);
            _nextAttack = false;
        }
    }

    public void OnClickAttack()
    {
        _nextAttack = true;
    }
}
