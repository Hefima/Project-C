using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject equipHead, equipTorso, equipBoots, equipHands, equipAmulete, equipRingLeft, equipRingRight, equipWeaponMain, equipWeaponSecc;



    public void Use(Item item)
    {
        switch (item.itemClass)
        {
            case Class.Weapon:
                Equip(item);
                break;
            case Class.Armor:
                Equip(item);
                break;
            case Class.Consumable:
                break;
            case Class.Item:
                break;
        }
    }

    void Equip(Item equipItem)
    {
        if(equipItem.itemClass == Class.Armor)
        {
            switch (equipItem.armorPart)
            {
                case ArmorPart.Head:
                    equipHead.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
                case ArmorPart.Torso:
                    equipTorso.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
                case ArmorPart.Amulete:
                    equipAmulete.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
                case ArmorPart.Ring:
                    if (equipRingRight.GetComponent<ItemSlot>().item == null)//right empty
                    {
                        equipRingRight.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                    else if(equipRingLeft.GetComponent<ItemSlot>().item == null)
                    {
                        equipRingLeft.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                    else
                    {
                        equipRingRight.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                    equipRingRight.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
                case ArmorPart.Boots:
                    equipBoots.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
                case ArmorPart.Hands:
                    equipHands.GetComponent<ItemSlot>().AddSlot(equipItem);
                    break;
            }
        }
        else if(equipItem.itemClass == Class.Weapon)
        {
            if (equipItem.twoHanded) //two Handed
            {
                equipWeaponMain.GetComponent<ItemSlot>().AddSlot(equipItem);

                equipWeaponSecc.GetComponent<ItemSlot>().ClearSlot(); //Clear Secc Slot
            }
            else //One Handed
            {
                if(equipWeaponMain.GetComponent<ItemSlot>().item == null) //Main empty
                {
                    equipWeaponMain.GetComponent<ItemSlot>().AddSlot(equipItem);
                }
                else //Main full
                {
                    if (equipWeaponMain.GetComponent<ItemSlot>().item.twoHanded || equipWeaponSecc.GetComponent<ItemSlot>().item != null) //Main zweihand || Secc full
                    {
                        equipWeaponMain.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                    else if(equipWeaponSecc.GetComponent<ItemSlot>().item == null)//Main einhand || Secc empty
                    {
                        equipWeaponSecc.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                    else
                    {
                        equipWeaponMain.GetComponent<ItemSlot>().AddSlot(equipItem);
                    }
                }
            }
        }
    }

}
