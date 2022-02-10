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
        if(item != null)
        {
            gameObject.name = item.name;
            if (item.prefab != null)
            {
                Instantiate(item.prefab, this.transform);
            }
            else
            {
                DebugManager.DebugLog("ItemPrefab missing: " + item.name, DebugType.ITEMDEBUG);
            }
        }
        else
        {
            DebugManager.DebugLog("Item Missing" + gameObject.name, DebugType.ITEMDEBUG);
            Destroy(gameObject);
        }

        //GetComponent<MeshFilter>().mesh = item.mesh;
        //GetComponent<MeshRenderer>().material = item.material;
    }
}
