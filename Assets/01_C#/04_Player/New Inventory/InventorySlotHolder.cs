using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHolder : MonoBehaviour
{
    public InventorySlot info = new InventorySlot(null, 1);

    public Image image;

    public void OnInstantiate()
    {
        info.slotObj = this.gameObject;
    }
}
