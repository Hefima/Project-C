using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public EnemyStats enemyStats;
    NavMeshAgent navMesh;

    public int currentHealth;

    void Start()
    {
        currentHealth = enemyStats.health;
        navMesh = GetComponent<NavMeshAgent>();

        if(navMesh != null)
        {
            navMesh.speed = enemyStats.moveSpeed;
        }
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
