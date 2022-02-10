using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AbilityHolder
{
    public int abilityID;
    public Ability ability;
    float cooldownTime;
    float activeTime;
    [HideInInspector]
    public MonoBehaviour parent;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState state = AbilityState.ready;

    public void SetParent(MonoBehaviour _parent)
    {
        parent = _parent;
        NewAbility();
    }

    public void NewAbility()
    {
        if (ability != null)
            ability.abilityHolder = this;
    }

    public void AbilityPressed()
    {
        if(state == AbilityState.ready && !PlayerManager.acc.PC.abilityActive)
        {
            ability.Activate();
            state = AbilityState.active;
            activeTime = ability.activeTime;
            parent.StartCoroutine(ActiveTime());
        }
    }

    IEnumerator ActiveTime()
    {
        PlayerManager.acc.PC.abilityActive = true;
        yield return new WaitForSeconds(activeTime);
        state = AbilityState.cooldown;
        cooldownTime = ability.coldownTime;
        parent.StartCoroutine(CooldownTime());
        PlayerManager.acc.PC.abilityActive = false;
    }

    IEnumerator CooldownTime()
    {
        GameManager.acc.UI.UpdateAbilityUI(abilityID, cooldownTime);
        yield return new WaitForSeconds(cooldownTime);
        state = AbilityState.ready;
    }
}
