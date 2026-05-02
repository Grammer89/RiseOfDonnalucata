using UnityEngine;

public class UI_MenuGame : MonoBehaviour
{
    [Header("GameObject MenuUI")]
    [SerializeField] private GameObject _uiMenu;
    [SerializeField] private GameObject _uiLeftMenu;
    [Header("GameObject Menu UI Prefab")]
    [SerializeField] private GameObject _uiHeroUiMenuPrefab;
    [SerializeField] private GameObject _missionPrefab;
    //[Header("Menu Item Configuration")]
    //[SerializeField] private GameObject _menuItem;
    //[SerializeField] private GameObject _itemPrefab;
    //[SerializeField] private RectTransform _contentItem;
    [Header("Menu Status")]
    [SerializeField] private GameObject _menuStatusLeft;
    [Header("Menu Mission")]
    [SerializeField] private GameObject _menuMission;

    //BottoneMenuItem
    //BottoneStatus
    //BottoneMission
    //BottonePlayer => Si aggancia a quello dello status
    private void Awake()
    {
        _uiMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_uiMenu.activeSelf) return;
            OnClickOpenMenu();
        }
    }
    public void OnClickOpenMenu()
    {
        _uiMenu.SetActive(true);
        _uiLeftMenu.SetActive(true);
        _uiHeroUiMenuPrefab.SetActive(true);
    }
    public void OnClickButtonExit()
    {
        _uiMenu.SetActive(false);
        _uiLeftMenu.SetActive(false);
    }
    //public void OnClickItem()
    //{
    //    _menuItem.SetActive(true);

    //}
    public void OnClickMissionMenu()
    {
        _menuMission.SetActive(true);
    }
    public void OnClickHeroUI()
    {
        _menuStatusLeft.SetActive(true);
    }

    //public void SetItemMenu()
    //{
    //    for (int i = 0; i < GameState.Instance.Inventory.Items.Count; i++)
    //    {
    //        GameObject item = Instantiate(_itemPrefab, _contentItem);
            
    //    }
    //}
}
