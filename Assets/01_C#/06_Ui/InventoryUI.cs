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
    }
}
