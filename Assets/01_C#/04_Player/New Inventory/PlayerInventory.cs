using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject slotPrefab;
    public Transform parentTransform;

    private void Start()
    {
        inventory.OnSlotCreate += OnSlotCreate;
    }

    public void OnSlotCreate(object sender, InventoryObject.OnSlotCreateEventArgs e)
    {
        GameObject g = Instantiate(slotPrefab, parentTransform);
        g.GetComponent<InventorySlotHolder>().info = e.slot;
        g.GetComponent<InventorySlotHolder>().info.slotObj = g;
        if (e.slot.item.image != null)
        {
            g.GetComponent<InventorySlotHolder>().image.sprite = e.slot.item.image;
            g.GetComponent<InventorySlotHolder>().image.enabled = true;
        }
        else
            GameManager.acc.DM.DebugLog("Item image Missing: " + e.slot.item.name, DebugType.ITEMDEBUG);
    }

    public void OnApplicationQuit()
    {
        inventory.inventorySlots.Clear();
    }
}
