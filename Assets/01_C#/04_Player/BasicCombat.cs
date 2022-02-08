using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCombat : MonoBehaviour, IBasicAttacks
{
    public Transform attackPoint;
    public float attackRange;
    public float attackCD;
    float attackBuffer;
    public float attackDuration;
    float nextAttack;
    public LayerMask enemyLayer;

    public int basicAttack;
    public int combo;
    int comboStep = 0;
    int comboAttackSpeedGained;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        nextAttack = Time.time;
        attackBuffer = Time.time;
    }

    public void BasicAttack()
    {      
        if (Time.time >= attackBuffer)
        {
            basicAttack = 0;
            combo = 0;
        }

        if(Time.time >= nextAttack)
        {
            CalculateComboBonus();

            attackCD = 1 / (PlayerManager.acc.basePlayerStats.baseAtkSpeed + (PlayerManager.acc.livePlayerStats.attackSpeed + PlayerManager.acc.livePlayerStats.bonusAttackSpeed) / 100);

            attackBuffer = Time.time + attackCD + attackDuration;
            nextAttack = Time.time + attackCD;

            this.gameObject.transform.rotation = Quaternion.Euler(0f, PlayerManager.acc.PM.cam.transform.eulerAngles.y, 0f);

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, PlayerManager.acc.PM.cam.transform.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Collider[] damagableHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            if (basicAttack == 0)
            {
                DamageEnemy(damagableHit);
                basicAttack ++;
            }
            else if(basicAttack == 1)
            {
                DamageEnemy(damagableHit);
                basicAttack++;
            }
            else if(basicAttack == 2)
            {
                DamageEnemy(damagableHit);
                basicAttack = 0;
            }
        }
    }

    void DamageEnemy(Collider[] damagable)
    {
        bool hit = false;
        for (int i = 0; i < damagable.Length; i++)
        {
            DebugManager.DebugLog("YOU HIT: " + damagable[i].name + "With" + basicAttack, DebugType.PLAYERDEBUG);
            damagable[i].GetComponent<IDamagable>().TakeDamage(PlayerManager.acc.livePlayerStats.attackDamage);
            hit = true;
        }
        if (hit)
            combo++;
        else
        {
            combo = 0;
            comboStep = 0;
            PlayerManager.acc.livePlayerStats.bonusAttackSpeed -= comboAttackSpeedGained;
            comboAttackSpeedGained = 0;
        }
    }

    void CalculateComboBonus()
    {
        bool newStep = false;
        if(combo == 10)
        {
            comboStep++;
            newStep = true;
        }
        else if(combo == 30)
        {
            comboStep++;
            newStep = true;
        }
        else if(combo == 50)
        {
            comboStep++;
            newStep = true;
        }

        if(newStep)
        {
            PlayerManager.acc.livePlayerStats.bonusAttackSpeed += combo / comboStep;
            comboAttackSpeedGained += combo / comboStep;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
