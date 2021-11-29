using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryUI;


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
