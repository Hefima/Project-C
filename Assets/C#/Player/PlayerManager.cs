using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager acc;

    //references
    public InputKeys IK;
    public PlayerCam Cam;
    public PlayerMove PM;
    public PlayerCombat PC;

    //PlayerInfo
    [SerializeField]
    public PlayerStats playerStats;

    void Awake()
    {
        acc = this;
    }
}
