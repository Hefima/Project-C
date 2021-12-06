using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySlotHolder equipTorso, equipBoots, equipGloves, equipAmulete, equipRingLeft, equipRingRight, equipWeaponMain, equipWeaponSecc;


    void EquipOld(Item equipItem)
    {
        if(equipItem.itemClass == Class.Armor)
        {
            switch (equipItem.armorPart)
            {
                case ArmorPart.Head:
                   // equipHead.GetComponent<ItemSlot>().AddSlot(equipItem);
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
                    //equipHands.GetComponent<ItemSlot>().AddSlot(equipItem);
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

    public void Equip(InventorySlot _slot)
    {
        switch (_slot.item.equipInfo.equipmentType)
        {
            case EquipmentType.OneHandWeapon:
                EquipWeapon(_slot);
                break;
            case EquipmentType.TwoHandWeapon:
                EquipWeapon(_slot);
                break;
            case EquipmentType.Amulete:
                break;
            case EquipmentType.Ring:
                if(EquipMain(equipRingRight, equipRingLeft))
                {
                    EquipSlot(equipRingRight, _slot);
                }
                else
                {
                    EquipSlot(equipRingLeft, _slot);
                }
                break;
            case EquipmentType.ChestArmor:
                EquipSlot(equipTorso, _slot);
                break;
            case EquipmentType.Gloves:
                break;
            case EquipmentType.Boots:
                EquipSlot(equipBoots, _slot);
                break;
            default:
                break;
        }
    }

    void EquipSlot(InventorySlotHolder _equipSlotHolder, InventorySlot _slot)
    {
        InventorySlot oldSlot = _slot;
        if (_slot.isEquiped)
        {
            _slot.isEquiped = false;
            PlayerManager.acc.PInv.inventory.AddItem(_slot.item);
            _equipSlotHolder.ClearSlotHolder();
        }
        else
        {
            PlayerManager.acc.PInv.inventory.RemoveItem(oldSlot);
            _slot.isEquiped = true;
            _equipSlotHolder.AddItem(_slot);
        }
    }

    bool EquipMain(InventorySlotHolder _mainEquipSlotHolder, InventorySlotHolder _seccEquipSlotHolder)
    {
        if(_mainEquipSlotHolder.info == null)
        {
            return true;
        }
        else if(_seccEquipSlotHolder.info == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void EquipWeapon(InventorySlot _slot)
    {
        if(_slot.item.equipInfo.equipmentType == EquipmentType.TwoHandWeapon)
        {
            EquipSlot(equipWeaponMain, _slot);

            if(equipWeaponSecc.info.item != null)
            {
                PlayerManager.acc.PInv.inventory.AddItem(equipWeaponSecc.info.item);
                equipWeaponSecc.ClearSlotHolder();
            }
        }
        else if(_slot.item.equipInfo.equipmentType == EquipmentType.OneHandWeapon)
        {
            if (EquipMain(equipWeaponMain, equipWeaponSecc))
            {
                EquipSlot(equipWeaponMain, _slot);
            }
            else
            {
                EquipSlot(equipWeaponSecc, _slot);
            }
        }
    }
    public void UpdateSlotUI(InventorySlotHolder _slotHolder)
    {
        if (_slotHolder.info.item.image != null)
        {
            _slotHolder.image.sprite = _slotHolder.info.item.image;
            _slotHolder.image.enabled = true;
        }
        else
            DebugManager.DebugLog("Item image Missing: " + _slotHolder.info.item.name, DebugType.ITEMDEBUG);
    }
}
