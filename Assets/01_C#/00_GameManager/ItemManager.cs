using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public List<EquipmentObject> equipmentObjects = new List<EquipmentObject>();
    public List<ConsumableObject> consumableObjects = new List<ConsumableObject>();
    public List<RessourceObject> ressourceObjects = new List<RessourceObject>();

    public GameObject itemPrefab;

    private void Start()
    {
        SetUpItems(ref equipmentObjects);
        SetUpItems(ref consumableObjects);
        SetUpItems(ref ressourceObjects);
    }

    void SetUpItems<T>(ref List<T> list) where T : ItemObject
    {
        list = list.Distinct().ToList();

        for (int i = 0; i < list.Count; i++)
        {
            string id;
            id = "" + (int)list[i].type + list[i].rarity + i;

            list[i].ID = int.Parse(id);
        }
    }

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

    public void DropItem(Transform position, ItemObject item)
    {
        GameObject g = Instantiate(itemPrefab, position.position, position.rotation);

        g.GetComponent<ItemHolder>().item = item;
    }
    public void DropItemParent(Transform position,Transform parent , ItemObject item)
    {
        GameObject g = Instantiate(itemPrefab, position.position, position.rotation, parent.parent);

        g.transform.parent = parent;

        g.GetComponent<ItemHolder>().item = item;
    }
}
