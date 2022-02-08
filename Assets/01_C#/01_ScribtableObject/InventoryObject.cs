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
    public void AddItem(ItemObject _item, int _amount = 1)
    {
        if(_amount > 1)
            GameManager.acc.EM.AddEvent("You recived: " + _item.name + "[" + _amount + "]");
        else
            GameManager.acc.EM.AddEvent("You recived: " + _item.name);

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
                    DebugManager.DebugLog("Cant carry more of this item", DebugType.ITEMDEBUG);
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

    public void RemoveItem(InventorySlot _slot, int _amount = 1)
    {
        if(_slot.amount - _amount <= 0)
        {
            inventorySlots.Remove(_slot);
            Destroy(_slot.slotHolder.gameObject);
        }
        else
        {
            _slot.amount -= _amount;
        }
    }
}


[System.Serializable]
public class InventorySlot
{
    public InventorySlotHolder slotHolder;
    public ItemObject item;
    public int amount;

    public bool isEquiped;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public InventorySlot(InventorySlot _slot)
    {
        slotHolder = _slot.slotHolder;

        isEquiped = _slot.isEquiped;

        item = _slot.item;
        amount = _slot.amount;
    }

    public void AddAmount(int _value)
    {
        amount += _value;
    }

    public void ClearSlot(bool clearSlotObj = false)
    {
        if (clearSlotObj)
        {
            slotHolder = null;
        }
        item = null;
        amount = 0;
    }
}
