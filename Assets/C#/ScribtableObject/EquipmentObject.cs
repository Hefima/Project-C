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
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public EquipmentType equipmentType;

    public float damage;
    public float attackSpeed;

    public float defense;
    public int health;
    public int Agility;
    public float Mana;
     
    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
