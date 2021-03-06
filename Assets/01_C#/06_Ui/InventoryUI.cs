using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void UpdateSlotUI(InventorySlotHolder _slotHolder)
    {
        if (_slotHolder.info.item.image != null)
        {
            _slotHolder.image.sprite = _slotHolder.info.item.image;
            _slotHolder.image.enabled = true;
        }
        else
            DebugManager.DebugLog("Item image Missing: " + _slotHolder.info.item.name, DebugType.ITEMDEBUG);

        if(_slotHolder.info.amount > 1)
        {
            _slotHolder.countTxt.text = _slotHolder.info.amount.ToString();
        }
        else
        {
            _slotHolder.countTxt.text = null;
        }

        if (_slotHolder.isEquipSlot)
        {
            _slotHolder.removeButton.interactable = false;
        }
    }
}
