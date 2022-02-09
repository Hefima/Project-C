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
    public List<ItemObject> itemRewards;
    public bool completed;

    public void CheckGoals()
    {
        completed = goals.All(g => g.goalCompleted);
    }

    public void GiveReward()
    {
        Debug.Log("Hunting Beni Done");
        PlayerManager.acc.GetExperience(experienceReward);
        //if(itemRewards.Count > 0)
        //{
        //    for (int i = 0; i < itemRewards.Count; i++)
        //    {
        //        PlayerManager.acc.PInv.inventory.AddItem(itemRewards[i]);
        //    }
        //}

    }
}
