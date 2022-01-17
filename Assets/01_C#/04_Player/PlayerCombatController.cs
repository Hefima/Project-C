using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //Class
    public PlayerBaseClass playerClass;


    public void AbilityI()
    {
        playerClass.AbilityI();
    }
    public void AbilityII()
    {
        playerClass.AbilityII();
    }
    public void AbilityIII()
    {
        playerClass.AbilityIII();
    }
    public void Ultimate()
    {
        playerClass.Ultimate();
    }
}
