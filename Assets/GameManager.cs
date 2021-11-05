using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager acc;

    public InputKeys IK;
    public UIManager UI;

    private void Awake()
    {
        acc = this;
    }
}
