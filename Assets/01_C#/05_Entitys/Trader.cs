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

    public void Start()
    {
        StartCoroutine(traderUi.CreateQuestUI(questType, this));
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
