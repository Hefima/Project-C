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

    public virtual IEnumerator Init()
    {
        //default
        yield return null;
    }


    public void CheckIfDone()
    {
        if(currentAmount >= requiredAmount)
        {
            Complete();
        }

        GameManager.acc.UI.questUI.UpdateQuestUI(quest.questPrefab, quest);
    }

    public void Complete()
    {
        goalCompleted = true;
        quest.CheckGoals();
        GameManager.acc.EM.AddEvent(description + ": done");
    }
}
