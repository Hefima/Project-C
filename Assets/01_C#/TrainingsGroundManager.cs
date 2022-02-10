using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingsGroundManager : MonoBehaviour
{
    public int maxFood;
    public GameObject foodDrop;
    public int maxEquipment;
    public GameObject equipmentDrop;

    public int currentFood;
    public int currentEquipment;
    public int currentEnemyI;
    public int currentEnemyII;



    public GameObject enemyIDrop;
    public int maxEnemyI;
    public GameObject enemyI;
    public int maxEnemyII;
    public GameObject enemyIIDrop;
    public GameObject enemyII;

    private void Update()
    {
        currentFood = foodDrop.transform.childCount;
        currentEquipment = equipmentDrop.transform.childCount;
        currentEnemyI = enemyIDrop.transform.childCount;
        currentEnemyII = enemyIIDrop.transform.childCount;

        if (currentFood < maxFood)
            DropFood();
        if (currentEquipment < maxEquipment)
            DropEquipment();
        if (currentEnemyI < maxEnemyI)
            DropEnemy(enemyI, enemyIDrop.transform);
        if (currentEnemyII < maxEnemyII)
            DropEnemy(enemyII, enemyIIDrop.transform);
    }

    public void DropFood()
    {
        GameManager.acc.IM.DropItemParent(foodDrop.transform, foodDrop.transform, GameManager.acc.IM.RandomItem_Type(ItemType.Consumable));
    }
    public void DropEquipment()
    {
        GameManager.acc.IM.DropItemParent(equipmentDrop.transform, equipmentDrop.transform, GameManager.acc.IM.RandomItem_Type(ItemType.Equipment));
    }
    public void DropEnemy(GameObject enemy, Transform position_parent)
    {
        Instantiate(enemy, position_parent);
    }
}
