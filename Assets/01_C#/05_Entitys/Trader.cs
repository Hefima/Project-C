using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public bool assigned;
    public bool helped;

    public Quest quest;
    public string questType;

    public void Interact()
    {
        if(!assigned && !helped)
        {
            AssignQuest();
        }
        else if(assigned && !helped)
        {
            CheckQuest();
        }
    }

    void AssignQuest()
    {
        assigned = true;
        quest = (Quest)PlayerManager.acc.avtiveQuests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if (quest.completed)
        {
            quest.GiveReward();
            helped = true;
            //assigned = false;
        }
    }
}
