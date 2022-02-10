using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinClass : PlayerBaseClass
{
    public override void AbilityI()
    {
        DebugManager.DebugLog("Assasin: I", DebugType.PLAYERDEBUG);
    }

    public override void AbilityII()
    {
        DebugManager.DebugLog("Assasin: II", DebugType.PLAYERDEBUG);
    }

    public override void AbilityIII()
    {
        DebugManager.DebugLog("Assasin: III", DebugType.PLAYERDEBUG);
    }

    public override void Ultimate()
    {
        DebugManager.DebugLog("Assasin: Ult", DebugType.PLAYERDEBUG);
    }
}
