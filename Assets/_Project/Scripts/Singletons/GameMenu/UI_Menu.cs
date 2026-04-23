using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : GenericSingleton<UI_Menu>
{
    [Header("MenuGameCanvas")]
    [SerializeField] private GameObject _canvasMenu;
    [Header("Left Menu")]
    [SerializeField] private GameObject _itemMenu;
    [SerializeField] private GameObject _statusMenu;
    [SerializeField] private GameObject _missionMenu;
    [SerializeField] private GameObject _configurationMenu;
    [SerializeField] private GameObject _saveMenu;

    [Header("Right Menu - Button")]
    [SerializeField] private Button _heroUIObjectButton;
    [SerializeField] private Button _itemMenuButton;
    [SerializeField] private Button _statusMenuButton;
    [SerializeField] private Button _missionMenuButton;
    [SerializeField] private Button _configurationMenuButton;
    [SerializeField] private Button _saveMenuButton;
    [SerializeField] private Button _exitMenuButton;

    [Header("Prefab Button")]
    [SerializeField] private GameObject _itemPrefab;
    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;
    }

    public void OnItemButtonClicked()
    {
        _itemMenu.SetActive(true);
        GameObject _itemPrefab = new GameObject();
        foreach (ItemInstance item in GameState.Instance.Inventory.Items)
        {
            
            //UI_ShopItemButton itemButton = Instantiate(_buttonPrefab, _buttonParents);
            //itemButton.Setup(_merchant.HealingItemToSell[i], OnSelected);
            //_shopButtons.Add(itemButton);
        }
    }
    public void OnStatusHeroClicked()
    { }

    public void OnStatusMissionClicked()
    { }

    public void OnConfigurationButtonClicked()
    { }

    public void OnSaveButtonClicked()
    { }

    public void OnExitButtonClicked()
    {
        _canvasMenu.SetActive(false);
    }


}
