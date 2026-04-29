using UnityEngine;
public enum ItemType
{
    None = 0,
    Weapon = 1,
    Armor = 2,
    Spell = 3,

    Potion = 99,
    Revify = 100
}
[CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
public class SO_GenericItem : ScriptableObject
{
    [SerializeField] private string _nameItem;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Sprite _imageItem;
    [SerializeField] private int _priceItem;
    [SerializeField] private int _healAmount;
    [SerializeField] private string _descriptionItem;
    
    public string NameItem => _nameItem;
    public Sprite ImageItem => _imageItem;
    public int PriceItem => _priceItem;
    public float HealAmount => _healAmount;
    public string DescriptionItem => _descriptionItem;
    public ItemType ItemType => _itemType;
}
