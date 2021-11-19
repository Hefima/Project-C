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
    public GameObject prefab;
    public int maxCarryAmount;

    [HideInInspector] public EquipmentObject equipInfo = null;
    [HideInInspector] public RessourceObject ressourceInfo = null;
    [HideInInspector] public ConsumableObject consumableInfo = null;
}
