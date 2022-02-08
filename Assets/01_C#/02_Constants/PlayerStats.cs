using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BasePlayerStats
{
    public int maxHealth;
    public int maxHealth_PL;
    [Space(5)]
    public float attackDamage;
    public float attackDamage_PL;
    [Space(5)]
    public float baseAtkSpeed;
    [Space(5)]
    public float attackSpeed;
    public float attackSpeed_PL;
    [Space(5)]
    public float defense;
    public float defense_PL;
    [Space(5)]
    public float agility;
}
[System.Serializable]
public struct LivePlayerStats
{
    public int maxHealth;

    public float attackDamage;

    public float attackSpeed;
    public int bonusAttackSpeed;

    public float defense;

    public float agility;
}
