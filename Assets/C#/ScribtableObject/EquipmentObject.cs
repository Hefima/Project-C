using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    OnHandWeapon,
    TwoHandWeapon,
    Amulete,
    Ring,
    ChestArmor,
    Gloves,
    Boots,
}

public class EquipmentObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Equipment;
    }
    EquipmentType equipmentType;

    public float damage;
    public float attackSpeed;

    public float defense;
    public int health;
}
