using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager acc;

    //references
    public PlayerCombat PC;
    public PlayerMove PM;
    public Inventory Inv;

    //PlayerInfo
    [SerializeField]
    public PlayerStats playerStats;

    void Awake()
    {
        if(acc != null)
        {
            Debug.LogWarning("More than one instance of PlayerManager found!");
            return;
        }
        acc = this;
    }
}
