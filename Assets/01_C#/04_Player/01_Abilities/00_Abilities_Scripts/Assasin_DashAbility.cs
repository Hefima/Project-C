using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DashAbility", menuName = "Ability System/Assasin/DashAbility")]
public class Assasin_DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate()
    {
        base.Activate();

        Vector3 moveDir = PlayerManager.acc.transform.forward * PlayerManager.acc.PM.input.y + PlayerManager.acc.transform.right * PlayerManager.acc.PM.input.x;

        PlayerManager.acc.PM.velocity = PlayerManager.acc.transform.forward * dashVelocity;

        abilityHolder.parent.StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        yield return new WaitUntil(() => abilityHolder.state == AbilityHolder.AbilityState.cooldown);
        PlayerManager.acc.PM.velocity = Vector3.zero;
    }
}
