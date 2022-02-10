using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingAngelo : Quest
{
    private void Start()
    {
        questName = "Angelos Hunt";
        description = "Kill 3 Angelo";
        experienceReward = 250;

        itemRewards.Add(GameManager.acc.IM.RandomItem());

        goals.Add(new KillGoal(this, 1, "Kill 3 Angelos", false, 0, 3));
        goals.Add(new CollectionGoal(this, 151, "Find a Legendary Katana", false, 0, 1));
        
        StartCoroutine(InitGoals());
    }
}
