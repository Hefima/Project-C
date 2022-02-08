using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public EnemyStats enemyStats;
    NavMeshAgent navMesh;

    public float currentHealth;

    void Start()
    {
        currentHealth = enemyStats.health;
        navMesh = GetComponent<NavMeshAgent>();

        if(navMesh != null)
        {
            navMesh.speed = enemyStats.moveSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
        GameManager.acc.EM.FireOnEnemyKilledEvent(this, new EventManager.OnEnemyKilledEventArgs { experience = enemyStats.experience });

        
    }
}
