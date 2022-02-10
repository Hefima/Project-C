using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingBeni : Quest
{
    private void Start()
    {
        questName = "Hunting Beni";
        description = "Kill 2 Beni";
        experienceReward = 100;

        itemRewards.Add(GameManager.acc.IM.RandomItem());

        goals.Add(new KillGoal(this, 0, "Kill 2 Benis", false, 0, 2));
        goals.Add(new CollectionGoal(this, 150, "Find Gojos Speed Boots", false, 0, 1));

        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].Init();
        }
        DebugManager.DebugLog("New Quest : " + questName, DebugType.QUESTDEBUG);
    }
}
