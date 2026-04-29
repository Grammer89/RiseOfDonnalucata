using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryState
{
    [SerializeField] private List<ItemInstance> _items = new List<ItemInstance>();
    [SerializeField] private int _money = 1000;

    public int Money
    {
        get => _money;
        set => _money = Mathf.Max(0, value); 
    }

    public List<ItemInstance> Items
    { get => _items; }

    public ItemInstance GetItem(int index)
    {  return _items[index]; }

    public bool HasItem(SO_GenericItem itemData) => HasItem(itemData, 1);

    public bool HasItem(SO_GenericItem itemData, int requiredAmount)
    {
        int index = FindItemIndex(itemData);
        if (index < 0)
        {
            return false;
        }
        return _items[index].Amount >= requiredAmount;
    }
    private int FindItemIndex(SO_GenericItem itemData)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Data == itemData)
            {
                return i;
            }
        }

        return -1;
    }

    public void AddItem(SO_GenericItem itemData, int amount)
    {
        if (amount < 1) return;

        int index = FindItemIndex(itemData);
        if (index < 0)
        {
            _items.Add(new ItemInstance(itemData, amount));
        }
        else
        {
            _items[index].Amount += amount;
        }
    }

    //public void RemoveItem(SO_HealingItem itemData, int amount)
    //{
    //    if (amount < 1) return;

    //    int index = FindItemIndex(itemData);
    //    if (index > 0)
    //    {
    //        _items.Remove(new ItemInstance(itemData, amount));
    //    }
    //    else
    //    {
    //        _items[index].Amount += amount;
    //    }
    //}
}