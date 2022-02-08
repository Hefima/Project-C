using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InventoryUI invUI;
    public StatsUI statsUI;

    public GameObject inventoryUIObject;
    public GameObject statsUIObject;

    //HealthBar
    public Slider healthSlider;
    public Text healthText;

    //Experience Bar
    public Slider expSlider;
    public Text expText;
    public Text levelText;

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

    public void UpdateHealthUI()
    {
        healthSlider.maxValue = PlayerManager.acc.livePlayerStats.maxHealth;
        healthSlider.value = PlayerManager.acc.currentHealth;
        healthText.text = Mathf.Round(PlayerManager.acc.currentHealth) + "/" + Mathf.Round(PlayerManager.acc.livePlayerStats.maxHealth);
    }

    public void UpdateExpUI()
    {
        expSlider.maxValue = PlayerManager.acc.playerExpMax;
        expSlider.value = PlayerManager.acc.playerExp;
        expText.text = Mathf.Round(PlayerManager.acc.playerExp) + "/" + Mathf.Round(PlayerManager.acc.playerExpMax);

        levelText.text = PlayerManager.acc.playerLevel.ToString();
    }
}
