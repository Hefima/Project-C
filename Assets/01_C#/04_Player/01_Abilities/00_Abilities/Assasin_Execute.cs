using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Execute", menuName = "Ability System/Assasin/Execute")]
public class Assasin_Execute : Ability
{
    public float attackRange;
    public float damageMultiplier;
    [Range(0,1)]
    public float executeThreshhold;
    public float executeDelay;

    public GameObject executeObj;


    public override void Activate()
    {
        base.Activate();

        abilityHolder.parent.StartCoroutine(Execute());
    }


    IEnumerator Execute()
    {
        PlayerManager.acc.gameObject.transform.rotation = Quaternion.Euler(0f, PlayerManager.acc.PM.moveRotation.transform.eulerAngles.y, 0f);

        PlayerManager.acc.PM.moveAllowed = false;

        GameObject g = Instantiate(executeObj, PlayerManager.acc.PC.BC.attackPoint.position, PlayerManager.acc.PC.BC.attackPoint.rotation);

        if(Physics.Raycast(PlayerManager.acc.PC.BC.attackPoint.position, PlayerManager.acc.PC.BC.attackPoint.forward, out var enemyHit, attackRange, PlayerManager.acc.PC.BC.enemyLayer))
        {
            IDamagable damagable = enemyHit.transform.GetComponent<IDamagable>();

            damagable.TakeDamage(PlayerManager.acc.livePlayerStats.attackDamage * damageMultiplier);

            yield return new WaitForSeconds(executeDelay);

            Destroy(g);
            PlayerManager.acc.PM.moveAllowed = true;

            if (enemyHit.transform != null)
            {
                Transform enemyTransform = enemyHit.transform;

                if (damagable.currentHealth <= damagable.baseStats.maxHealth * executeThreshhold)
                {
                    damagable.TakeDamage(damagable.baseStats.maxHealth);
                    //teleport to him
                    PlayerManager.acc.transform.position = enemyTransform.position;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(executeDelay);
            Destroy(g);
            PlayerManager.acc.PM.moveAllowed = true;
        }
        yield return null;
    }
}
