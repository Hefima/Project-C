using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryUI invUI;

    public GameObject inventoryUIObject;


    public void ToggleUI(GameObject ui)
    {
        if (ui.activeInHierarchy)
        {
            ui.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            ui.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
