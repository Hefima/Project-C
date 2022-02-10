using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float coldownTime;
    public float activeTime;

    public AbilityHolder abilityHolder;

    public virtual void Activate()
    {
        //Default
    }
}
