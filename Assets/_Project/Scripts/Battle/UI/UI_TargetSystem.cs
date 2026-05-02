using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TargetSystem : MonoBehaviour
{
    [SerializeField] private GameObject _uiTargetSystem;
    [SerializeField] private RectTransform _contentTarget;
    [SerializeField] private Button _prefabButtonTarget;


    private void Awake()
    {
        _uiTargetSystem.SetActive(false);
       
    }

    public void EnableCanvas()
    {
        SetupTargetSystem();
        _uiTargetSystem.SetActive(true);
    }

    public void SetupTargetSystem()
    {
         for (int i = 0; i < BattleManager.Instance.OrderBattle.Count; i++)
        {
            if (BattleManager.Instance.OrderBattle[i].Creature.CreatureType == HeroType.Enemy &&
                BattleManager.Instance.OrderBattle[i].ActualHP <= 0) { continue; }
                Button buttonTarget = Instantiate(_prefabButtonTarget, _contentTarget);

            if (BattleManager.Instance.OrderBattle[i].ActualHP <= 0 )
            {
                buttonTarget.interactable = false;

            }
            TextMeshProUGUI text = buttonTarget.GetComponentInChildren<TextMeshProUGUI>();
            Image[] icon = buttonTarget.GetComponentsInChildren<Image>();

            if (text != null)
            {
                text.SetText(BattleManager.Instance.OrderBattle[i].NameCreature);
            }
            if (icon != null)
            {
                icon[1].sprite = BattleManager.Instance.OrderBattle[i].Icon;
            }
            buttonTarget.onClick.AddListener(() => OnClickTarget(text.text));
        }

    }

    public void OnClickTarget(string target)
    {
        BattleManager.Instance.TargetName = target;
        BattleManager.Instance.CalculateAttack = true;
        Debug.Log("Verrà attaccato: " + target);

        RemoveButton();
        _uiTargetSystem.SetActive(false);
       

    }
    public void RemoveButton()
    {
        Button[] button = _contentTarget?.GetComponentsInChildren<Button>();
        if (button != null)
        {
            Debug.Log("Rimuovo i bottoni");
            for (int i = 0; i < button.Length; i++)
            {
                DestroyImmediate(button[i].gameObject);
            }
        }
    }


}

