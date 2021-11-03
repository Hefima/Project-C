using UnityEngine;

[CreateAssetMenu(fileName = "New Ressource Object", menuName = "Inventory System/Items/Ressource")]
public class RessourceObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Ressource;
    }
}
