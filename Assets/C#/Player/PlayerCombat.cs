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

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

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

            this.gameObject.transform.rotation = Quaternion.Euler(0f, PlayerManager.acc.PM.cam.transform.eulerAngles.y, 0f);

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, PlayerManager.acc.PM.cam.transform.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            if (basicAttack == 0)
            {
                DamageEnemy(enemiesHit);
                basicAttack ++;
            }
            else if(basicAttack == 1)
            {
                DamageEnemy(enemiesHit);
                basicAttack++;
            }
            else if(basicAttack == 2)
            {
                DamageEnemy(enemiesHit);
                basicAttack = 0;
            }
        }
    }

    void DamageEnemy(Collider[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("YOU HIT: " + enemies[i].name + "With" + basicAttack);
            enemies[i].GetComponent<EnemyManager>().TakeDamage(PlayerManager.acc.playerStats.attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
