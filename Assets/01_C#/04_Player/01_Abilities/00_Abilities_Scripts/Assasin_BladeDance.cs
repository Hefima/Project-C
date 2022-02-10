using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BladeDance", menuName = "Ability System/Assasin/BladeDance")]
public class Assasin_BladeDance : Ability
{
    public float defenceBonus;

    public float attackRadius;
    public float damageMultiplier;

    public float timeBetweenDmg;
    public int timesAttacking;

    public override void Activate()
    {
        base.Activate();
        activeTime = timeBetweenDmg * timesAttacking;

        abilityHolder.parent.StartCoroutine(BladeDance());
    }


    IEnumerator BladeDance()
    {
        PlayerManager.acc.livePlayerStats.defense += defenceBonus;
        PlayerManager.acc.PM.moveAllowed = false;
        for (int i = 0; i < timesAttacking; i++)
        {
            Collider[] damagableHit = Physics.OverlapSphere(PlayerManager.acc.transform.position, attackRadius, PlayerManager.acc.PC.BC.enemyLayer);
            for (int ii = 0; ii < damagableHit.Length; ii++)
            {                
                damagableHit[ii].GetComponent<IDamagable>().TakeDamage(PlayerManager.acc.livePlayerStats.attackDamage * damageMultiplier);
            }

            yield return new WaitForSeconds(timeBetweenDmg);
        }

        PlayerManager.acc.livePlayerStats.defense -= defenceBonus;
        PlayerManager.acc.PM.moveAllowed = true;

        yield return null;
    }
}
