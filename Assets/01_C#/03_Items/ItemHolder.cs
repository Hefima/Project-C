using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public ItemObject item;

    private void Start()
    {
        ItemInstantiate();
    }

    public void ItemInstantiate()
    {
        gameObject.name = item.name;

        if (item.prefab != null)
            Instantiate(item.prefab, this.transform);
        else
            DebugManager.DebugLog("ItemPrefab missing: " + item.name, DebugType.ITEMDEBUG);
        //GetComponent<MeshFilter>().mesh = item.mesh;
        //GetComponent<MeshRenderer>().material = item.material;
    }
}
