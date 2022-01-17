using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    public static PlayerManager acc;

    //references
    public PlayerMove PM;
    public PlayerInventory PInv;
    public PlayerCombatController PC;

    //PlayerInfo
    [SerializeField]
    public PlayerStats playerStats;
    public int currentHealth;

    //Others
    int ticks = 0;
    bool eating;

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
        currentHealth = playerStats.maxHealth;
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

    public void GetHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > playerStats.maxHealth)
        {
            currentHealth = playerStats.maxHealth;
        }
    }

    private void Die()
    {
        DebugManager.DebugLog("U DIED!", DebugType.PLAYERDEBUG);
    }

    public void AddFood(int _restoreHealthValue, int _tickAmount, float _restoreTickTime)
    {
        if(!eating)
            StartCoroutine(AddFoodCourutine(_restoreHealthValue, _tickAmount, _restoreTickTime));
    }

    IEnumerator AddFoodCourutine(int _restoreHealthValue, int _tickAmount, float _restoreTickTime)
    {
        eating = true;
        PlayerManager.acc.GetHealth(_restoreHealthValue / _tickAmount);

        yield return new WaitForSeconds(_restoreTickTime);
        ticks++;
        if (ticks < _tickAmount)
        {
            StartCoroutine(AddFoodCourutine(_restoreHealthValue, _tickAmount, _restoreTickTime));
        }
        else
        {
            eating = false;
            ticks = 0;
        }
        yield break;
    }
}
