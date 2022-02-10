using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : QuestGoal
{
    public int itemID;

    public CollectionGoal(Quest _quest, int _itemID, string _description, bool _completed, int _currentAmount, int _requiredAmount)
    {
        quest = _quest;
        itemID = _itemID;
        description = _description;
        goalCompleted = _completed;
        currentAmount = _currentAmount;
        requiredAmount = _requiredAmount;
    }

    public override IEnumerator Init()
    {
        GameManager.acc.EM.OnItemPickedUp += ItemPickedUp;
        CheckPlayerInv();
        yield return null;
    }

    void ItemPickedUp(object sender, EventManager.OnItemPickedUpEventArgs e)
    {
        if(e.item.ID == this.itemID)
        {
            currentAmount++;
            CheckIfDone();
            Debug.Log(currentAmount);
        }
    }

    void CheckPlayerInv()
    {
        var itemSearch = PlayerManager.acc.PInv.inventory.SearchItem(itemID);
        if (itemSearch.hasItem)
        {
            currentAmount += itemSearch.amount;
            CheckIfDone();
        }        
    }
}
