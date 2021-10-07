using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager acc;

    public InputKeys IK;

    private void Awake()
    {
        acc = this;
    }
}
