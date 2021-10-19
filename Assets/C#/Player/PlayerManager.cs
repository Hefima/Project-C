using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager acc;

    //references
    public PlayerCombat PC;
    public NewMove PM;

    //PlayerInfo
    [SerializeField]
    public PlayerStats playerStats;

    void Awake()
    {
        acc = this;
    }
}
