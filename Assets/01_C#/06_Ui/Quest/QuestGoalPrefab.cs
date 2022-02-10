using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGoalPrefab : MonoBehaviour
{
    public Text goalDescription;
    public Slider goalProgress;
    public GameObject goalComplete;

    public void SetGoalUi(string _goalDescription, float _maxAmount, bool _goalComplete, float _currentAmount = 0f)
    {
        goalDescription.text = _goalDescription;

        goalProgress.maxValue = _maxAmount;
        goalProgress.value = _currentAmount;

        goalComplete.SetActive(_goalComplete);
    }

    public void UpdateGoalProgress(float _currentAmount, bool _goalComplete)
    {
        goalProgress.value = _currentAmount;
        goalComplete.SetActive(_goalComplete);
    }
}
