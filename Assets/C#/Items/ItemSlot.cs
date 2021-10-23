using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public bool hasItem = false;
    public Image image;

    public void AddSlot(Item newItem)
    {
        if(newItem != null)
        {
            hasItem = true;
            item = newItem;
            image.sprite = item.image;
            image.enabled = true;
        }
    }

    public void ClearSlot()
    {
        hasItem = false;
        item = null;
        image.enabled = false;
    }
}
