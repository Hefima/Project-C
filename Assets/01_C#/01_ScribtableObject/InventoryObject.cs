using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName = "Inventory System/Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public event EventHandler<OnSlotCreateEventArgs> OnSlotCreate;
    public class OnSlotCreateEventArgs : EventArgs
    {
        public InventorySlot slot;
    }
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == _item && _item.type != ItemType.Equipment)
            {
                if(inventorySlots[i].amount + _amount <= _item.maxCarryAmount)
                {
                    inventorySlots[i].AddAmount(_amount);
                }
                else
                {
                    GameManager.acc.DM.DebugLog("Cant carry more of this item", DebugType.ITEMDEBUG);
                }
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            InventorySlot newSlot = new InventorySlot(_item, _amount);
            inventorySlots.Add(newSlot);
            OnSlotCreate?.Invoke(this, new OnSlotCreateEventArgs { slot = newSlot });
        }
    }

    public void RemoveItem(InventorySlot slot, int _amount)
    {
        if(slot.amount - _amount <= 0)
        {
            inventorySlots.Remove(slot);
            Destroy(slot.slotObj);
        }
        else
        {
            slot.amount -= _amount;
        }
    }
}


[System.Serializable]
public class InventorySlot
{
    public GameObject slotObj;
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int _value)
    {
        amount += _value;
    }
}
