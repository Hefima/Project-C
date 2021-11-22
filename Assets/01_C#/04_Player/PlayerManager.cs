using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager acc;

    //references
    public PlayerCombat PC;
    public PlayerMove PM;
    public PlayerInventory Inv;
    public PlayerInventory PInv;

    //PlayerInfo
    [SerializeField]
    public PlayerStats playerStats;
    public int currentHealth;

    void Awake()
    {
        if(acc != null)
        {
            DebugManager.DebugLog("More than one instance of PlayerManager found!", DebugType.DEFAULTDEBUG);
            return;
        }
        acc = this;
    }

    private void Start()
    {
        currentHealth = playerStats.health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        DebugManager.DebugLog("U DIED", DebugType.PLAYERDEBUG);
    }
}
