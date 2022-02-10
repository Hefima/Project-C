using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderUi : MonoBehaviour
{
    public Transform questParent;
    public GameObject questPrefab;
    public GameObject buttonPrefab;

    public IEnumerator CreateQuestUI(string _questName, Trader trader)
    {
        GameObject quest = Instantiate(questPrefab, questParent);

        QuestPrefab questHolder = quest.GetComponent<QuestPrefab>();

        questHolder.questName.text = _questName;
        questHolder.questDescription.text = "";

        GameObject button = Instantiate(buttonPrefab, quest.transform);

        ButtonHolder btnHolder = button.GetComponent<ButtonHolder>();

        trader.questBtn = btnHolder;

        ChangeQuestButton(btnHolder, "Accept");

        trader.AssigntButton();

        yield return null;
        //Forces UI to Update wierd Bug where ui doesent Update
        if (!questParent.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            yield return null;
            gameObject.SetActive(false);
            yield return null;
        }
        //Ich hab kein fix gefunden 1 tag vor abgabe man (forceUpdate ging nicht) help :(   
    }

    public void ChangeQuestButton(ButtonHolder button, string _text)
    {
        button.buttonText.text = _text;
    }
}
