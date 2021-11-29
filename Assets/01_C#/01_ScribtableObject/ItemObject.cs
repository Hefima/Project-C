using UnityEngine;
public enum ItemType
{
    Default,
    Equipment,
    Consumable,
    Ressource,
}
public abstract class ItemObject: ScriptableObject
{
    public new string name;
    public ItemType type;
    public int maxCarryAmount = 1;

    [Header("GFX")]
    public GameObject prefab;
    public Sprite image;

    [HideInInspector] public EquipmentObject equipInfo = null;
    [HideInInspector] public RessourceObject ressourceInfo = null;
    [HideInInspector] public ConsumableObject consumableInfo = null;

    public abstract void Use();
}
