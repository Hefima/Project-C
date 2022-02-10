using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHolder : MonoBehaviour
{
    public InventorySlot info = null;
    public bool hasItem = false;
    public bool isEquipSlot = false;
    public Image image;
    public Text countTxt;
    public Button removeButton;

    public void OnInstantiate()
    {
        info.slotHolder = this;
        GameManager.acc.UI.invUI.UpdateSlotUI(this);
        if(!isEquipSlot)
            removeButton.onClick.AddListener(OnRemoveButtonClick);
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

    void OnRemoveButtonClick()
    {
        if (GameManager.acc.IK.input_CTRL)
        {
            PlayerManager.acc.PInv.inventory.RemoveItem(info, info.amount);
        }
        else
        {
            PlayerManager.acc.PInv.inventory.RemoveItem(info);
            GameManager.acc.UI.invUI.UpdateSlotUI(this);
        }
    }
}
