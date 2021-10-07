using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public float attackCD;
     float attackBuffer;
    public float attackDuration;
    float nextAttack;
    public LayerMask enemyLayer;

    public int basicAttack;


    private void Start()
    {
        nextAttack = Time.time;
        attackBuffer = Time.time;
    }

    void Update()
    {
        attackCD = 1 / (PlayerManager.acc.playerStats.baseAtkSpeed + PlayerManager.acc.playerStats.attackSpeed / 100);

        if (GameManager.acc.IK.input_Mouse0)
            BasicAttack();
    }

    void BasicAttack()
    {
        

        if (Time.time >= attackBuffer)
        {
            basicAttack = 0;
        }

        if(Time.time >= nextAttack)
        {
            attackBuffer = Time.time + attackDuration;
            nextAttack = Time.time + attackCD;

            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            if (basicAttack == 0)
            {
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    Debug.Log("YOU HIT: " + enemiesHit[i].name + "With" + basicAttack);
                }
                basicAttack ++;
            }
            else if(basicAttack == 1)
            {
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    Debug.Log("YOU HIT: " + enemiesHit[i].name + "With" + basicAttack);
                }
                basicAttack++;
            }
            else if(basicAttack == 2)
            {
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    Debug.Log("YOU HIT: " + enemiesHit[i].name + "With" + basicAttack);
                }
                basicAttack = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
