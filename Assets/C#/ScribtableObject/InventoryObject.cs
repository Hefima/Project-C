using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName = "Inventory System/Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == _item)
            {
                if(inventorySlots[i].amount + _amount !> _item.maxCarryAmount)
                {
                    inventorySlots[i].AddAmount(_amount);
                }
                else
                {
                    Debug.Log("Cant carry more of this item");
                }
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            inventorySlots.Add(new InventorySlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
