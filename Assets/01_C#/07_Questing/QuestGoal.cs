using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal
{
    public Quest quest;
    public string description;
    public bool goalCompleted;
    public int currentAmount;
    public int requiredAmount;

    public virtual void Init()
    {
        //default
    }


    public void CheckIfDone()
    {
        if(currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        goalCompleted = true;
        quest.CheckGoals();
        GameManager.acc.EM.AddEvent(description + ": done");
    }
}
