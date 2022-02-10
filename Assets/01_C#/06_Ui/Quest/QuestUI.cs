using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public Transform questParent;

    public GameObject questPrefab;
    public GameObject goalPrefab;

    public IEnumerator CreateQuestUI<T> (T _quest) where T : Quest
    {
        GameObject quest = Instantiate(questPrefab, questParent);
        yield return null;
        QuestPrefab questHolder = quest.GetComponent<QuestPrefab>();

        _quest.questPrefab = questHolder;

        questHolder.questName.text = _quest.questName;
        questHolder.questDescription.text = _quest.description;

        for (int i = 0; i < _quest.goals.Count; i++)
        {
            GameObject goal = Instantiate(goalPrefab, quest.transform);
            yield return null;
            QuestGoalPrefab goalHolder = goal.GetComponent<QuestGoalPrefab>();

            goalHolder.SetGoalUi(_quest.goals[i].description, _quest.goals[i].requiredAmount, _quest.goals[i].goalCompleted, _quest.goals[i].currentAmount);

            questHolder.goals.Add(goalHolder);
        }

        yield return null;
        //Forces UI to Update wierd Bug where ui doesent Update
        if (!questParent.gameObject.activeInHierarchy)
        {
            GameManager.acc.UI.questUIObject.SetActive(true);
            yield return null;
            GameManager.acc.UI.questUIObject.SetActive(false);
        }
        else //Ich hab kein fix gefunden 1 tag vor abgabe man (forceUpdate ging nicht) help :(
        {
            GameManager.acc.UI.questUIObject.SetActive(false);
            yield return null;
            GameManager.acc.UI.questUIObject.SetActive(true);
        }
    }

    public void UpdateQuestUI(QuestPrefab _questPrefab, Quest _quest)
    {
        _questPrefab.UpdateQuestUI(_quest);
    }
}
