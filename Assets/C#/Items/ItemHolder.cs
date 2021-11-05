using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public ItemObject item;

    private void Start()
    {
        this.gameObject.name = item.name;

        if (item.prefab != null)
            Instantiate(item.prefab, this.transform);
        //GetComponent<MeshFilter>().mesh = item.mesh;
        //GetComponent<MeshRenderer>().material = item.material;
    }
}
