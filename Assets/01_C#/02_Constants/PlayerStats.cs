using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BaseStats
{
    public int ID;
    [Space(5)]
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

    public BaseStats(int _ID, int _maxHealth, int _maxHealth_Pl,float _attackDamage,
        float _attackDamage_PL, float _baseAtkSpeed, float _attackSpeed, float _attackSpeed_PL,
        float _defense, float _defense_PL, float _agility)
    {
        ID = _ID;

        maxHealth = _maxHealth;
        maxHealth_PL = _maxHealth_Pl;

        attackDamage = _attackDamage;
        attackDamage_PL = _attackDamage_PL;

        baseAtkSpeed = _baseAtkSpeed;

        attackSpeed = _attackSpeed;
        attackSpeed_PL = _attackSpeed_PL;

        defense = _defense;
        defense_PL = _defense_PL;

        agility = _agility;
    }
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
