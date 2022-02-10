using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHolder : MonoBehaviour
{
    public ToolTipTrigger toolTipTrigger;

    public InventorySlot info = null;
    public bool hasItem = false;
    public bool isEquipSlot = false;
    public Image image;
    public Text countTxt;
    public Button removeButton;

    public void OnInstantiate()
    {
        info.slotHolder = this;
        GameManager.acc.UI.invUI.UpdateSlotUI(this);
        if(!isEquipSlot)
            removeButton.onClick.AddListener(OnRemoveButtonClick);

        SetToolTip();
    }

    void SetToolTip()
    {
        if (info.item != null)
        {
            toolTipTrigger.header = info.item.name;
            switch (info.item.type)
            {
                case ItemType.Default:
                    break;
                case ItemType.Equipment:
                    toolTipTrigger.content = "\n" +
                        info.item.equipInfo.equipmentType.ToString() + "\n" +
                        "\n" +
                        "Health : " + info.item.equipInfo.health + "\n" +
                        "Damage : " + info.item.equipInfo.damage + "\n" +
                        "Atk Speed : " + info.item.equipInfo.attackSpeed + "\n" +
                        "Defense :  " + info.item.equipInfo.defense + "\n" +
                        "Agility : " + info.item.equipInfo.Agility + "\n";
                    break;
                case ItemType.Consumable:
                    float timeToRestore = info.item.consumableInfo.restoreTickTime * info.item.consumableInfo.tickAmount;

                    toolTipTrigger.content = "\n" +
                        info.item.type.ToString() + "\n" +
                        "Health restored : " + info.item.consumableInfo.restoreHealthValue + "\n" +
                        "Time to restore : " + timeToRestore.ToString("0.0") + "secc";

                    break;
                case ItemType.Ressource:
                    toolTipTrigger.content = "\n" +
                        info.item.type.ToString() + "\n" +
                        info.item.ressourceInfo.ressourceDescription;
                    break;
                default:
                    break;
            }
        }
        else
        {
            toolTipTrigger.header = "";
            toolTipTrigger.content = "";
        }

    }

    void ChangeToolTipContent()
    {

    }

    public void UsePressed()
    {
        if(info.item != null)
            info.item.Use(info);
    }

    public void AddItem(InventorySlot _slot)
    {
        hasItem = true;
        info = _slot;
        OnInstantiate();
    }

    public void ClearSlotHolder()
    {
        hasItem = false;
        image.enabled = false;
        image.sprite = null;
        info = null;
    }

    void OnRemoveButtonClick()
    {
        if (GameManager.acc.IK.input_CTRL)
        {
            PlayerManager.acc.PInv.inventory.RemoveItem(info, info.amount);
        }
        else
        {
            PlayerManager.acc.PInv.inventory.RemoveItem(info);
            GameManager.acc.UI.invUI.UpdateSlotUI(this);
        }
    }
}
