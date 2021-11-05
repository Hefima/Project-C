using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var item = other.GetComponent<ItemHolder>();
        if (item && GameManager.acc.IK.input_E)
        {
            PlayerManager.acc.PInv.inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
