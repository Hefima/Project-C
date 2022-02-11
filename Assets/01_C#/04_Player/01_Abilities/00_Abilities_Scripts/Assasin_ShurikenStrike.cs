using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShurikenStrike", menuName = "Ability System/Assasin/ShurikenStrike")]
public class Assasin_ShurikenStrike : Ability
{
    public float dashVelocity;
    public float jumpVelocityMultiplier;
    public float dashTime;
    //Shuriken
    public GameObject shuriken;
    public int shurikenAmount;
    public float shurikenSpeed;
    public float timeBetweenShuriken;
    public float shurikenDamageMultiplier;

    public override void Activate()
    {
        base.Activate();

        abilityHolder.parent.StartCoroutine(ShootShuriken());
    }


    IEnumerator ShootShuriken()
    {
        int shuriken = 0;

        PlayerManager.acc.PM.moveAllowed = false;

        //Dash Back
        PlayerManager.acc.PM.velocity = -PlayerManager.acc.PM.moveRotation.transform.forward * dashVelocity;
        abilityHolder.parent.StartCoroutine(EndDash(dashTime));

        //Jump (when Cam Fixed to player)
        PlayerManager.acc.PM.isJumping = true;
        PlayerManager.acc.PM.velocity.y = PlayerManager.acc.PM.initialJumpVelocity * jumpVelocityMultiplier;

        PlayerManager.acc.gameObject.transform.rotation = Quaternion.Euler(0f, PlayerManager.acc.PM.moveRotation.transform.eulerAngles.y, 0f);

        for (int i = 0; i < shurikenAmount; i++)
        {
            shuriken++;
            FireShuriken();

            yield return new WaitForSeconds(timeBetweenShuriken);
        }

        PlayerManager.acc.PM.moveAllowed = true;
    }

    IEnumerator EndDash( float time)
    {
        yield return new WaitForSeconds(time);
        PlayerManager.acc.PM.velocity = Vector3.zero;
    }

    void FireShuriken()
    {
        GameObject g = Instantiate(shuriken, PlayerManager.acc.PC.BC.attackPoint.position, Camera.main.transform.rotation);
        Shuriken s = g.GetComponent<Shuriken>();

        s.speed = shurikenSpeed;
        s.damage = PlayerManager.acc.livePlayerStats.attackDamage * shurikenDamageMultiplier;
    }
}
