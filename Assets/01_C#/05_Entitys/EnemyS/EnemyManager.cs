using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public BaseStats enemyBaseStats;
    public BaseStats baseStats
    {
        get { return enemyBaseStats; }
        set { enemyBaseStats = value; }
    }
    public int experience;

    public float attackRange;
    NavMeshAgent navMesh;

    public float enemyCurrentHealth;
    public float currentHealth
    {
        get { return enemyCurrentHealth; }
    }

    public Slider healthSlider;

    void Start()
    {
        enemyCurrentHealth = baseStats.maxHealth;
        navMesh = GetComponent<NavMeshAgent>();

        if(navMesh != null)
        {
            navMesh.speed = baseStats.agility;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
            Die();
        UpdateHealthUI();
    }

    void Die()
    {
        GameManager.acc.IM.DropItem(transform, GameManager.acc.IM.RandomItem());

        GameManager.acc.EM.FireOnEnemyKilledEvent(this, new EventManager.OnEnemyKilledEventArgs { experience = this.experience, enemyID = baseStats.ID });        
        Destroy(this.gameObject);
    }

    void UpdateHealthUI()
    {
        healthSlider.maxValue = baseStats.maxHealth;
        healthSlider.value = enemyCurrentHealth;
    }
}
