using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        /*GetComponent<MeshFilter>().mesh = item.itemObj.GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().material = item.itemObj.GetComponent<MeshRenderer>().material;*/

        this.gameObject.name = item.name;

        GetComponent<MeshFilter>().mesh = item.mesh;
        GetComponent<MeshRenderer>().material = item.material;
    }
}
