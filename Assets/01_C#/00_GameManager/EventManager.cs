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
    public int unit = 50;

    public float eventActiveTime = 5;
    float eventDeathtime;

    public GameObject eventPrefab;
    public Transform eventParent;

    bool waitRunning;

    int test;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddEvent("teseasfd" + test);
            test++;
        }
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

        GameObject g;

        if(waitingList.Count > 0)
        {
            g = waitingList[0];            
            waitingList.Remove(g);
        }
        else
        {
            g = Instantiate(eventPrefab, eventParent);
        }
        activeEvents.Add(g);
        g.SetActive(true);
        g.transform.position = eventParent.transform.position;
        g.transform.GetChild(0).GetComponent<Text>().text = eventList[0];
        eventList.Remove(eventList[0]);



        if (activeEvents.Count > 1)
        {
            for (int i = 0; i < activeEvents.Count -1; i++)
            {
                activeEvents[i].transform.position = new Vector3(activeEvents[i].transform.position.x, activeEvents[i].transform.position.y + unit, 0);
            }
        }

        if (activeEvents.Count > 5)
        {
            activeEvents[0].SetActive(false);
            waitingList.Add(activeEvents[0]);
            activeEvents.Remove(activeEvents[0]);
        }

        if (waitRunning)
            eventDeathtime = Time.time + eventActiveTime;
        else
            StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        waitRunning = true;
        eventDeathtime = Time.time + eventActiveTime;

        yield return new WaitUntil(() => Time.time > eventDeathtime);

        ClearActiveEvents();
        waitRunning = false;
        yield break;
    }

    void ClearActiveEvents()
    {
        for (int i = 0; i < activeEvents.Count; i++)
        {
            activeEvents[i].SetActive(false);
            waitingList.Add(activeEvents[i]);
        }
        activeEvents.Clear();
    }
}
