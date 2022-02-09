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
    public PlayerAnimations PA;

    //PlayerInfo
    [SerializeField]
    public BasePlayerStats basePlayerStats;
    [SerializeField]
    public LivePlayerStats livePlayerStats;
    public float currentHealth;
    //Level
    public int playerLevel;
    public float playerExp;
    public float playerExpMax = 100;
    public float levelIncrease = 1.8f;
    //Quests
    public GameObject avtiveQuests;
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
        GetLivePlayerStats();

        currentHealth = basePlayerStats.maxHealth;
        GameManager.acc.UI.UpdateHealthUI();
        GameManager.acc.UI.UpdateExpUI();

        GameManager.acc.EM.OnEnemyKilled += OnEnemyKilled;
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

    public void GetLivePlayerStats()
    {
        livePlayerStats.maxHealth = basePlayerStats.maxHealth + (basePlayerStats.maxHealth_PL * playerLevel) + PInv.GetEquipHealth();
        livePlayerStats.attackDamage = basePlayerStats.attackDamage + (basePlayerStats.attackDamage_PL * playerLevel) + PInv.GetEquipDamage();
        livePlayerStats.attackSpeed = basePlayerStats.attackSpeed + (basePlayerStats.attackSpeed_PL * playerLevel) + PInv.GetEquipAttackSpeed();
        livePlayerStats.defense = basePlayerStats.defense + (basePlayerStats.defense_PL * playerLevel) + PInv.GetEquipDefence();
        livePlayerStats.agility = basePlayerStats.agility + PInv.GetEquipAgility();

        GameManager.acc.UI.UpdateHealthUI();
        GameManager.acc.UI.statsUI.UpdateStatsUI();
    }

    public void GetExperience(float exp)
    {
        playerExp += exp;
        if(playerExp >= playerExpMax)
        {
            LevelUp(playerExp - playerExpMax);
        }

        GameManager.acc.UI.UpdateExpUI();

        GameManager.acc.EM.AddEvent("Experience: " + exp);
    }

    public void LevelUp(float expOverflow)
    {
        playerLevel++;
        playerExp = expOverflow;

        playerExpMax = playerExpMax * levelIncrease;

        GameManager.acc.UI.UpdateExpUI();
        GetLivePlayerStats();
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = damage / (1 + livePlayerStats.defense / 100);

        currentHealth -= actualDamage;
        GameManager.acc.UI.UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    public void GetHealth(float health)
    {
        currentHealth += health;       
        if (currentHealth > basePlayerStats.maxHealth)
        {
            currentHealth = basePlayerStats.maxHealth;
        }
        GameManager.acc.UI.UpdateHealthUI();
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

    public void OnEnemyKilled(object sender, EventManager.OnEnemyKilledEventArgs e)
    {
        GetExperience(e.experience);
    }
}
