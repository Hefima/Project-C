using UnityEngine;

[CreateAssetMenu(fileName = "New Ressource Object", menuName = "Inventory System/Items/Ressource")]
public class RessourceObject : ItemObject
{
    [Header("Ressource Values")]
    public string ressourceDescription;
    private void Awake()
    {
        type = ItemType.Ressource;
        ressourceInfo = this;
    }
}
