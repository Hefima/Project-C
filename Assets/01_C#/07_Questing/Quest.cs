using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    [SerializeField]
    public List<QuestGoal> goals = new List<QuestGoal>();
    public string questName;
    [TextArea]
    public string description;
    public int experienceReward;
    public List<ItemObject> itemRewards = new List<ItemObject>();
    public bool completed;

    public void CheckGoals()
    {
        completed = goals.All(g => g.goalCompleted);
    }

    public void GiveReward()
    {
        DebugManager.DebugLog("Quest :" + questName + " is Complete");
        PlayerManager.acc.GetExperience(experienceReward);
        if(itemRewards.Count != 0)
        {
            for (int i = 0; i < itemRewards.Count; i++)
            {
                PlayerManager.acc.PInv.inventory.AddItem(itemRewards[i]);
            }
        }
        GameManager.acc.EM.AddEvent("Quest : " + questName + " completed");

        Destroy(this);
    }
}
