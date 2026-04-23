using UnityEngine;

[CreateAssetMenu(fileName = "New Merchant", menuName = "Data/Merchant")]
public class SO_Merchant : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private SO_GenericItem[] _healingItemsToSell;

    public string Name => _name;
    public SO_GenericItem[] HealingItemToSell => _healingItemsToSell;
}
