using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    [Header("Consumable Values")]
    public int restoreHealthValue;

    public void Awake()
    {
        type = ItemType.Consumable;
        consumableInfo = this;
    }
}
