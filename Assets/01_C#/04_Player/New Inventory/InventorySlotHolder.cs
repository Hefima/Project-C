using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHolder : MonoBehaviour
{
    public InventorySlot info = null;
    public bool hasItem = false;
    public Image image;
    public void OnInstantiate()
    {
        info.slotHolder = this;
        GameManager.acc.UI.invUI.UpdateSlotUI(this);
    }

    public void UsePressed()
    {
        info.item.Use(info);
    }

    public void AddItem(InventorySlot _slot)
    {
        hasItem = true;
        info = _slot;
        OnInstantiate();
    }

    public void ClearSlotHolder()
    {
        hasItem = false;
        image.enabled = false;
        image.sprite = null;
        info = null;
    }
}
