using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    public static PlayerManager acc;

    //references
    //public PlayerCombat PC;
    public PlayerMove PM;
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

    private void Update()
    {
        PM.PlayerMoveUpdate();

        if (GameManager.acc.IK.input_Mouse0)
            GetComponent<IBasicAttacks>().BasicAttack();
    }

    private void FixedUpdate()
    {
        PM.PlayerMoveFixedUpdate();
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
