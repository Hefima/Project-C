using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Collectable" && Input.GetKeyDown(KeyCode.E))
        {
            print("collect: " + other.name);
            PlayerManager.acc.Inv.Add(other.GetComponent<ItemHolder>().item);
            Destroy(other.gameObject);
        }
    }
}
