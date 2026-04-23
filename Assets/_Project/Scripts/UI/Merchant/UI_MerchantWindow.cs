using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MerchantWindow : MonoBehaviour
{
    [SerializeField] private UI_ShopItemButton _buttonPrefab;
    [SerializeField] private Transform _buttonParents;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TextMeshProUGUI _merchantNameText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemPriceText;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private Image _itemIcon;

    private SO_Merchant _merchant;
    private SO_GenericItem _selectedItem;
    private List<UI_ShopItemButton> _shopButtons = new();
    public void Setup(SO_Merchant merchant)
    {
        _merchant = merchant;

        _merchantNameText.SetText(_merchant.Name);

        ////foreach( var itemToSell in _merchant.HealingItemToSell)
        ////{
        ////    UI_ShopItemButton itemButton = Instantiate(_buttonPrefab, _buttonParents);
        ////    itemButton.Setup( itemToSell, OnSelected);
        ////}
        //Gestione ottimizzazione bottoni per casistica più mercanti con oggetti differenti
        int i = 0;
        for (; i < _merchant.HealingItemToSell.Length && i < _shopButtons.Count; i++)
        {
            _shopButtons[i].Setup(_merchant.HealingItemToSell[i], OnSelected);
            _shopButtons[i].gameObject.SetActive(true);
        }
        for (; i < _merchant.HealingItemToSell.Length; i++)
        {
            UI_ShopItemButton itemButton = Instantiate(_buttonPrefab, _buttonParents);
            itemButton.Setup(_merchant.HealingItemToSell[i], OnSelected);
            _shopButtons.Add(itemButton);
        }
        for (; i < _shopButtons.Count; i++)
        {
            _shopButtons[i].gameObject.SetActive(false);
        }

        Color colorItem = Color.white;
        colorItem.a = 0;
        _itemIcon.color = colorItem;

        gameObject.SetActive(true);
    }

    public void OnSelected(SO_GenericItem itemData)
    {
        _selectedItem = itemData;
        RefreshGraphics();
    }

    public void OnBuyClicked()
    {
        if (GameState.Instance.Inventory.Money >= _selectedItem.PriceItem)
        {
            GameState.Instance.Inventory.Money -= _selectedItem.PriceItem;
            GameState.Instance.Inventory.AddItem(_selectedItem, 1);
            RefreshGraphics();
        }
    }

    public void OnExitClicked()
    {

        gameObject.SetActive(false);
        _itemDescriptionText.SetText(" ");
        _itemNameText.SetText(" ");
        _itemPriceText.SetText(" ");
        _buyButton.interactable = false;
    }
    public void RefreshGraphics()
    {
        bool canBuy = _selectedItem != null && GameState.Instance.Inventory.Money >= _selectedItem.PriceItem;


        _buyButton.interactable = canBuy;

        if (_selectedItem != null)
        {
            _itemDescriptionText.SetText(_selectedItem.DescriptionItem);
            _itemNameText.SetText(_selectedItem.NameItem);
            _itemPriceText.SetText(_selectedItem.PriceItem.ToString());
            if (_itemIcon.color.a == 0)
            {
                Color colorItem = Color.white;
                colorItem.a = 1;
                _itemIcon.color = colorItem;
            }
            _itemIcon.sprite = _selectedItem.ImageItem;

        }


    }
}
