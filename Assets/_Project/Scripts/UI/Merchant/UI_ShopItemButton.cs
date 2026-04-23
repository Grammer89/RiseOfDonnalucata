using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_ShopItemButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _descriptionItem;
    private SO_GenericItem _data;
    private Action<SO_GenericItem> _onSelect;

    public void Setup(SO_GenericItem item , Action<SO_GenericItem> onSelect)
    {
        _onSelect = onSelect;
        _data = item;
        _image.sprite = _data.ImageItem;
        _nameText.SetText(_data.NameItem);
        _priceText.SetText(_data.PriceItem.ToString());
        _descriptionItem.SetText(_data.DescriptionItem);
    }

    public void OnClick()
    {
        _onSelect?.Invoke(_data);
    }    
}
