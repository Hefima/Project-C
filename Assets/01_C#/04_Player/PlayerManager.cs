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
    public BaseStats playerBaseStats;
    public BaseStats baseStats
    {
        get { return playerBaseStats; }
        set { playerBaseStats = value; }
    }

    [SerializeField]
    public LivePlayerStats livePlayerStats;
    public float playerCurrentHealth;
    public float currentHealth
    {
        get { return playerCurrentHealth; }
    }
    //Level
    public int playerLevel;
    public float playerExp;
    public float playerExpMax = 100;
    public float levelIncrease = 1.8f;
    //Quests
    public GameObject avtiveQuests;
    //Others
    int ticks = 0;
    public bool eating;

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
        playerCurrentHealth = baseStats.maxHealth;
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
        livePlayerStats.maxHealth = baseStats.maxHealth + (baseStats.maxHealth_PL * playerLevel) + PInv.GetEquipHealth();
        livePlayerStats.attackDamage = baseStats.attackDamage + (baseStats.attackDamage_PL * playerLevel) + PInv.GetEquipDamage();
        livePlayerStats.attackSpeed = baseStats.attackSpeed + (baseStats.attackSpeed_PL * playerLevel) + PInv.GetEquipAttackSpeed();
        livePlayerStats.defense = baseStats.defense + (baseStats.defense_PL * playerLevel) + PInv.GetEquipDefence();
        livePlayerStats.agility = baseStats.agility + PInv.GetEquipAgility();

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


        playerExpMax = playerExpMax * levelIncrease;

        GameManager.acc.UI.UpdateExpUI();
        GetLivePlayerStats();

        GetExperience(expOverflow);
        GameManager.acc.EM.AddEvent("Level UP " + playerLevel);
        return;
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = damage / (1 + livePlayerStats.defense / 100);

        playerCurrentHealth -= actualDamage;
        GameManager.acc.UI.UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    public void GetHealth(float health)
    {
        playerCurrentHealth += health;       
        if (currentHealth > livePlayerStats.maxHealth)
        {
            playerCurrentHealth = livePlayerStats.maxHealth;
        }
        GameManager.acc.UI.UpdateHealthUI();
    }

    public void TeleportPlayer(Vector3 position)
    {
        PM.controller.enabled = false;
        transform.position = position;
        PM.controller.enabled = true;
    }

    private void Die()
    {
        DebugManager.DebugLog("U DIED!", DebugType.PLAYERDEBUG);
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        GameManager.acc.UI.ToggleUI(GameManager.acc.UI.gameOver);
        Time.timeScale = 0;
        PlayerManager.acc.PInv.inventory.inventorySlots.Clear();
        yield return new WaitForSecondsRealtime(3);
        GameManager.acc.SH.LoadScene(2);
        yield return null;
    }
    public bool AddFood(int _restoreHealthValue, int _tickAmount, float _restoreTickTime)
    {
        if (!eating)
        {
            StartCoroutine(AddFoodCourutine(_restoreHealthValue, _tickAmount, _restoreTickTime));
            return true;
        }
        else
        {
            return false;
        }
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

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(PC.BC.attackPoint.position, PC.BC.attackPoint.position + PC.BC.attackPoint.forward * 10);
    }
}
