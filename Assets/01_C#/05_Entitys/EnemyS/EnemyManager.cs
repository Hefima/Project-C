using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public EnemyStats enemyStats;
    NavMeshAgent navMesh;

    public float currentHealth;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = enemyStats.health;
        navMesh = GetComponent<NavMeshAgent>();

        if(navMesh != null)
        {
            navMesh.speed = enemyStats.moveSpeed;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
        UpdateHealthUI();
    }

    void Die()
    {
        Destroy(this.gameObject);
        GameManager.acc.EM.FireOnEnemyKilledEvent(this, new EventManager.OnEnemyKilledEventArgs { experience = enemyStats.experience, enemyID = enemyStats.ID });        
    }

    void UpdateHealthUI()
    {
        healthSlider.maxValue = enemyStats.health;
        healthSlider.value = currentHealth;
    }
}
