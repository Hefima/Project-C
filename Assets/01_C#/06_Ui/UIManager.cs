using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InventoryUI invUI;
    public StatsUI statsUI;
    public QuestUI questUI;
    public ToolTipSystem toolTip;

    public GameObject inventoryUIObject;
    public GameObject statsUIObject;
    public GameObject questUIObject;
    public GameObject pauseUIObject;

    public GameObject UICanvas;

    public GameObject gameOver;

    //HealthBar
    public Slider healthSlider;
    public Text healthText;

    //Experience Bar
    public Slider expSlider;
    public Text expText;
    public Text levelText;

    //Abilities
    public Image abiilityI;
    public Image abiilityII;
    public Image abiilityIII;
    public Image ultimate;

    public void ToggleUI(GameObject ui)
    {
        if (ui.activeInHierarchy)
        {
            ui.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.acc.UI.toolTip.Hide();
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

    public void UpdateAbilityUI(int _abilityID, float _cooldown)
    {
        switch (_abilityID)
        {
            default:
                break;
            case 1:
                StartCoroutine(UseAbility(abiilityI, _cooldown));
                break;
            case 2:
                StartCoroutine(UseAbility(abiilityII, _cooldown));
                break;
            case 3:
                StartCoroutine(UseAbility(abiilityIII, _cooldown));
                break;
            case 4:
                StartCoroutine(UseAbility(ultimate, _cooldown));
                break;
        }
    }

    IEnumerator UseAbility(Image _abilityImage, float _cooldown)
    {
        _abilityImage.fillAmount = 1;

        float elapsedTime = 0;
        float waitTime = _cooldown;

        while (_abilityImage.fillAmount > 0)
        {
            _abilityImage.fillAmount = Mathf.Lerp(1, 0, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }

    public void TogglePause()
    {
        if (pauseUIObject.activeInHierarchy)
        {
            pauseUIObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        else
        {
            pauseUIObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}
