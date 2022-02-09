using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyStats
{
    public int ID;

    public int health;
    public int attackDamage;

    public float baseAtkSpeed;
    public float attackSpeed;

    public float moveSpeed;

    public float attackRange;

    public int experience;
}
