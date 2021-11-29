using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    [Header("Consumable Values")]
    public int restoreHealthValue;
    public float restoreTickTime;
    public int tickAmount;


    public void Awake()
    {
        type = ItemType.Consumable;
        consumableInfo = this;
    }

    public override void Use()
    {
        DebugManager.DebugLog(name, DebugType.ITEMDEBUG);
        PlayerManager.acc.AddFood(restoreHealthValue, tickAmount, restoreTickTime);
    }
}
