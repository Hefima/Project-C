using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animator;

    public IEnumerator Punsh(string punsh)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("punsh_speed",  1 + 1 / (1 / (PlayerManager.acc.basePlayerStats.baseAtkSpeed + PlayerManager.acc.livePlayerStats.attackSpeed / 100)));
        animator.SetTrigger(punsh);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(1).IsName(punsh));
        yield return StartCoroutine(WaitForAnimationFinish(animator.GetCurrentAnimatorStateInfo(1).length));

        animator.SetLayerWeight(1, 0);
        yield return null;
    }

    IEnumerator WaitForAnimationFinish(float _delay = 0)
    {
        yield return new WaitForSeconds(_delay);
    }
}
