using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [SerializeField] private SO_GenericItem _data;
    [SerializeField] private int _amount;

    //Set & Get
    public SO_GenericItem Data => _data;

    public int Amount
    {
        get => _amount;
        set => _amount = Mathf.Max(0, value);
    }

    //Constructor
    public ItemInstance(SO_GenericItem itemData, int amount)
    {
        _data = itemData;
        _amount = amount;
    }
 
}
