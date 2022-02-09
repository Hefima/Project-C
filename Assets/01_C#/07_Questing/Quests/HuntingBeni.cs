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

        goals.Add(new KillGoal(this, 0, "Kill 2 Beni", false, 0, 2));

        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].Init();
        }
    }
}
