using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Consumable;
    }

    public int restoreHealthValue;
}
