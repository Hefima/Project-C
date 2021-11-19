using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName = "Inventory System/Inventory/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public event EventHandler<OnAddItemEventArgs> OnAddItem;
    public class OnAddItemEventArgs : EventArgs
    {
        public ItemObject item;
        public int amount;
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
                    Debug.Log("Cant carry more of this item");
                }
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Debug.Log("Inv Add");
            InventorySlot newSlot = new InventorySlot(_item, _amount);
            inventorySlots.Add(newSlot);
            OnAddItem?.Invoke(this, new OnAddItemEventArgs { item = _item, amount = _amount, slot = newSlot});
        }
    }

    public void RemoveItem()
    {

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
