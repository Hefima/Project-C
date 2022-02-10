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

        PlayerManager.acc.PM.velocity = PlayerManager.acc.PM.cam.transform.forward * dashVelocity;

        abilityHolder.parent.StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        yield return new WaitUntil(() => abilityHolder.state == AbilityHolder.AbilityState.cooldown);
        PlayerManager.acc.PM.velocity = Vector3.zero;
    }
}
