using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public List<string> eventList = new List<string>();
    public List<GameObject> activeEvents = new List<GameObject>();
    public List<GameObject> waitingList = new List<GameObject>();
    public int maxEventsDisplayed = 5;
    public int maxWaitingListEvents = 20;
    public int unit = 50;

    public float eventActiveTime = 5;

    public GameObject eventPrefab;
    public Transform eventParent;

    public event EventHandler<OnEnemyKilledEventArgs> OnEnemyKilled;
    public class OnEnemyKilledEventArgs : EventArgs
    {
        public int enemyID;
        public int experience;
    }

    public void FireOnEnemyKilledEvent(object sender, OnEnemyKilledEventArgs onEnemyKilledEventArgs)
    {
        OnEnemyKilled?.Invoke(sender, onEnemyKilledEventArgs);
    }

    public void AddEvent(string _content)
    {
        eventList.Add(_content);
        DisplayEvent();
    }

    public void DisplayEvent()
    {
        if (eventList[0] == null)
            return;

        GameObject newEvent;

        if(waitingList.Count > 0)
        {
            newEvent = waitingList[0];            
            waitingList.Remove(newEvent);
        }
        else
        {
            newEvent = Instantiate(eventPrefab, eventParent);
        }
        activeEvents.Add(newEvent);
        newEvent.SetActive(true);
        newEvent.transform.position = eventParent.transform.position;
        newEvent.transform.GetChild(0).GetComponent<Text>().text = eventList[0];
        eventList.Remove(eventList[0]);

        if (activeEvents.Count > 1)
        {
            for (int i = 0; i < activeEvents.Count -1; i++)
            {
                activeEvents[i].transform.position = new Vector3(activeEvents[i].transform.position.x, activeEvents[i].transform.position.y + unit, 0);
            }
        }

        StartCoroutine(Wait(newEvent));

        if (activeEvents.Count > maxEventsDisplayed)
        {
            activeEvents[0].SetActive(false);
            activeEvents.Remove(activeEvents[0]);
        }
    }

    IEnumerator Wait(GameObject _g)
    {
        yield return new WaitForSeconds(eventActiveTime);

        _g.SetActive(false);
        if(waitingList.Count <= maxWaitingListEvents)
            waitingList.Add(_g);
        activeEvents.Remove(_g);

        yield break;
    }
}
