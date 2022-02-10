using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public BasicCombat BC;

    public AbilityHolder abilityI;
    public AbilityHolder abilityII;
    public AbilityHolder abilityIII;
    public AbilityHolder ultimate;

    public bool abilityActive = false;

    private void Start()
    {
        SettAllParents();
    }

    public void SettAllParents()
    {
        abilityI.SetParent(this);
        abilityI.abilityID = 1;
        abilityII.SetParent(this);
        abilityII.abilityID = 2;
        abilityIII.SetParent(this);
        abilityIII.abilityID = 3;
        ultimate.SetParent(this);
        ultimate.abilityID = 4;
    }

    public void AbilityI()
    {
        if (abilityI != null)
            abilityI.AbilityPressed();
    }
    public void AbilityII()
    {
        if (abilityII != null)
            abilityII.AbilityPressed();
    }
    public void AbilityIII()
    {
        if (abilityIII != null)
            abilityIII.AbilityPressed();
    }
    public void Ultimate()
    {
        if(ultimate != null)
            ultimate.AbilityPressed();
    }
}
