using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public List<ItemObject> equipedItems;

    public GameObject slotPrefab;
    public Transform parentTransform;

    public InventorySlotHolder equipTorso, equipBoots, equipGloves, equipAmulete, equipRingLeft, equipRingRight, equipWeaponMain, equipWeaponSecc;

    public List<InventorySlotHolder> equipSlots = new List<InventorySlotHolder>();

    private void Start()
    {
        inventory.OnSlotCreate += OnSlotCreate;
        AddEquipSlots();
    }

    void AddEquipSlots()
    {
        equipSlots.Add(equipTorso);
        equipSlots.Add(equipBoots);
        equipSlots.Add(equipGloves);
        equipSlots.Add(equipAmulete);
        equipSlots.Add(equipRingLeft);
        equipSlots.Add(equipRingRight);
        equipSlots.Add(equipWeaponMain);
        equipSlots.Add(equipWeaponSecc);
    }

    public void OnSlotCreate(object sender, InventoryObject.OnSlotCreateEventArgs e)
    {
        GameObject g = Instantiate(slotPrefab, parentTransform);
        g.GetComponent<InventorySlotHolder>().info = e.slot;
        g.GetComponent<InventorySlotHolder>().OnInstantiate();
    }

    public void OnApplicationQuit()
    {
        inventory.inventorySlots.Clear();
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
                if (EquipMain(equipRingRight, equipRingLeft))
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
            _slot.slotHolder.ClearSlotHolder();
        }
        else
        {
            if (_equipSlotHolder.hasItem)
            {
                _equipSlotHolder.info.isEquiped = false;
                oldSlot.slotHolder.AddItem(_equipSlotHolder.info);

                _slot.isEquiped = true;
                _equipSlotHolder.AddItem(_slot);
            }
            else
            {
                PlayerManager.acc.PInv.inventory.RemoveItem(oldSlot);
                _slot.isEquiped = true;
                _equipSlotHolder.AddItem(_slot);
            }
        }
    }

    bool EquipMain(InventorySlotHolder _mainEquipSlotHolder, InventorySlotHolder _seccEquipSlotHolder)
    {
        if (!_mainEquipSlotHolder.hasItem)
        {
            return true;
        }
        else if (!_seccEquipSlotHolder.hasItem)
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
        if (_slot.item.equipInfo.equipmentType == EquipmentType.TwoHandWeapon)
        {
            EquipSlot(equipWeaponMain, _slot);

            if (equipWeaponSecc.hasItem)
            {
                PlayerManager.acc.PInv.inventory.AddItem(equipWeaponSecc.info.item);
                equipWeaponSecc.ClearSlotHolder();
            }
        }
        else if (_slot.item.equipInfo.equipmentType == EquipmentType.OneHandWeapon)
        {
            if (EquipMain(equipWeaponMain, equipWeaponSecc))
            {
                EquipSlot(equipWeaponMain, _slot);
            }
            else
            {
                if (equipWeaponMain.info.item.equipInfo.equipmentType == EquipmentType.TwoHandWeapon)
                {
                    EquipSlot(equipWeaponMain, _slot);
                }
                else
                {
                    EquipSlot(equipWeaponSecc, _slot);
                }
            }
        }
    }
}
