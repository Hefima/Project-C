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
        inventory.OnAddItem += OnSlotCreate;
    }
    public void OnSlotCreate(object sender, InventoryObject.OnAddItemEventArgs e)
    {
        Debug.Log("Instantiate");
        GameObject g = Instantiate(slotPrefab, parentTransform);
        g.GetComponent<InventorySlotHolder>().info = new InventorySlot(e.item, e.amount);
    }

    public void OnApplicationQuit()
    {
        inventory.inventorySlots.Clear();
    }
}
