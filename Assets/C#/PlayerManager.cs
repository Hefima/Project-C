using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager acc;

    //references
    public InputKeys IK;
    public PlayerCam PC;
    public PlayerMove PM;

    void Awake()
    {
        acc = this;
    }
}
