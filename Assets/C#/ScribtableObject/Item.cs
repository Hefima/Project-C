using UnityEngine;

public enum Class
{
    Weapon,
    Armor,
    Consumable,
    Item
}
public enum ArmorPart
{
    Head,
    Torso,
    Amulete,
    Ring,
    Boots,
    Hands
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description;

    [Header("Class")]
    public Class itemClass;
    public ArmorPart armorPart;

    public bool twoHanded;


    [Header("GFX")]  
    public Sprite image = null;

    public GameObject itemObj;

    public Mesh mesh;
    public Material material;
}
