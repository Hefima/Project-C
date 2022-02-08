using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<RessourceObject> ressourceObjects = new List<RessourceObject>();
    public List<ConsumableObject> consumableObjects = new List<ConsumableObject>();
    public List<EquipmentObject> equipmentObjects = new List<EquipmentObject>();

    public GameObject itemPrefab;

    public ItemObject RandomItem_Type(ItemType itemType)
    {
        int random = 1;
        switch (itemType)
        {
            case ItemType.Default:
                break;
            case ItemType.Equipment:
                random = Random.Range(0, equipmentObjects.Count);
                return equipmentObjects[random];
            case ItemType.Consumable:
                random = Random.Range(0, consumableObjects.Count);
                return consumableObjects[random];
            case ItemType.Ressource:
                random = Random.Range(0, ressourceObjects.Count);
                return ressourceObjects[random];
        }
        return null;
    }

    public ItemObject RandomItem()
    {
        int random = Random.Range(1,3);
        switch (random)
        {
            case 1:
                return RandomItem_Type(ItemType.Equipment);
            case 2:
                return RandomItem_Type(ItemType.Consumable);
            case 3:
                return RandomItem_Type(ItemType.Ressource);
        }
        return null;
    }

    public void DropItem(Transform transform, ItemObject item)
    {
        GameObject g = Instantiate(itemPrefab, transform.position, transform.rotation);
    }
}
