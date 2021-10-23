using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> invItems = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public GameObject invUi;
    public Transform itemSlotParent;
    public GameObject itemSlot;

    public void Add (Item item)
    {
        invItems.Add(item);

        GameObject g = Instantiate(itemSlot, itemSlotParent);
        g.GetComponent<ItemSlot>().AddSlot(item);

        if(onItemChangedCallback != null)
        onItemChangedCallback.Invoke(); 
    }

    public void Remove (Item item)
    {
        invItems.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void ToggleInv()
    {
        if (invUi.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            invUi.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            invUi.SetActive(true);
        }
        
    }
}
