using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName = "Inventory System/Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
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
                    GameManager.acc.EM.FireOnItemPickedUpEvent(this, new EventManager.OnItemPickedUpEventArgs { item = _item });
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
            GameManager.acc.EM.FireOnSlotCreateEvent(this, new EventManager.OnSlotCreateEventArgs { slot = newSlot });
            GameManager.acc.EM.FireOnItemPickedUpEvent(this, new EventManager.OnItemPickedUpEventArgs { item = _item });
        }
    }

    public void CreateSlot(InventorySlot _slot)
    {
        GameManager.acc.EM.FireOnSlotCreateEvent(this, new EventManager.OnSlotCreateEventArgs { slot = _slot });
    }

    public void RemoveItem(InventorySlot _slot, int _amount = 1)
    {
        if(_slot.amount - _amount <= 0)
        {
            inventorySlots.Remove(_slot);
            Destroy(_slot.slotHolder.gameObject);
            GameManager.acc.UI.toolTip.Hide();
        }
        else
        {
            _slot.amount -= _amount;
            GameManager.acc.UI.invUI.UpdateSlotUI(_slot.slotHolder);
        }
    }

    public (bool hasItem, int amount) SearchItem(ItemObject _item)
    {
        bool hasItem = false;
        int amount = 0;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item.ID == _item.ID)
            {
                hasItem = true;
                amount += inventorySlots[i].amount;
            }
        }

        return (hasItem, amount);
    }
    public (bool hasItem, int amount) SearchItem(int _itemID)
    {
        bool hasItem = false;
        int amount = 0;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item.ID == _itemID)
            {
                hasItem = true;
                amount += inventorySlots[i].amount;
            }
        }

        return (hasItem, amount);
    }
    public (bool hasItem, int amount) SearchItem(string _itemName)
    {
        bool hasItem = false;
        int amount = 0;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item.name == _itemName)
            {
                hasItem = true;
                amount += inventorySlots[i].amount;
            }
        }

        return (hasItem, amount);
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
        GameManager.acc.UI.invUI.UpdateSlotUI(slotHolder);
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
