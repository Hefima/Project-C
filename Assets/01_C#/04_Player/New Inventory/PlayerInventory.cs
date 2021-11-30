using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public List<ItemObject> equipedItems;

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
        g.GetComponent<InventorySlotHolder>().OnInstantiate();
    }

    public void OnApplicationQuit()
    {
        inventory.inventorySlots.Clear();
    }
}
