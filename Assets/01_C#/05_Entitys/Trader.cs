using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour, IInteractable
{
    public bool assigned;
    public bool helped;

    public Quest quest;
    public string questType;

    //Ui
    public ButtonHolder questBtn;
    public TraderUi traderUi;
    public GameObject traderUIObject;

    public void Start()
    {
        StartCoroutine(Initialize());
    }


    IEnumerator Initialize()
    {
        yield return StartCoroutine(CreateCanvas());

        yield return StartCoroutine(traderUi.CreateQuestUI(questType, this));
    }
    IEnumerator CreateCanvas()
    {
        GameObject g = Instantiate(traderUIObject, GameManager.acc.UI.UICanvas.transform);

        traderUi = g.GetComponent<TraderUi>();
        yield return null;
    }
    public void AssigntButton()
    {
        questBtn.btn.onClick.AddListener(ButtonPressed);
    }

    public void Interact()
    {
        GameManager.acc.UI.ToggleUI(traderUi.gameObject);

        PlayerManager.acc.PM.moveAllowed = !PlayerManager.acc.PM.moveAllowed;
    }

    public void ButtonPressed()
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
        traderUi.ChangeQuestButton(questBtn, "Finish");
        GameManager.acc.EM.AddEvent("Quest : " + questType + " accepted");
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
