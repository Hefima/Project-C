using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPrefab : MonoBehaviour
{
    public Text questName;
    public Text questDescription;

    public List<QuestGoalPrefab> goals;

    public void SetQuestUI(string _questName, string _description)
    {
        questName.text = _questName;
        questDescription.text = _description;
    }

    public void UpdateQuestUI(Quest _quest)
    {
        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].UpdateGoalProgress(_quest.goals[i].currentAmount, _quest.goals[i].goalCompleted);
        }
    }
}
