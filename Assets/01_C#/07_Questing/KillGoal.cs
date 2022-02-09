using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : QuestGoal
{
    public int enemyID;

    public KillGoal(Quest _quest, int _enemyID, string _description, bool _completed, int _currentAmount, int _requiredAmount)
    {
        quest = _quest;
        enemyID = _enemyID;
        description = _description;
        goalCompleted = _completed;
        currentAmount = _currentAmount;
        requiredAmount = _requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        GameManager.acc.EM.OnEnemyKilled += EnemyDied;
    }

    void EnemyDied(object sender, EventManager.OnEnemyKilledEventArgs e)
    {
        if(e.enemyID == this.enemyID)
        {
            currentAmount++;
            CheckIfDone();
            Debug.Log(currentAmount);
        }
    }
}
