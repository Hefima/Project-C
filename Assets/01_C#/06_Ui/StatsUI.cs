using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text healthUI, damageUI, atkSpeedUI, defenceUI, agilityUI, dpsUI;

    public void UpdateStatsUI()
    {
        healthUI.text = PlayerManager.acc.livePlayerStats.maxHealth.ToString("0.00");
        damageUI.text = PlayerManager.acc.livePlayerStats.attackDamage.ToString("0.00");
        atkSpeedUI.text = PlayerManager.acc.livePlayerStats.attackSpeed.ToString("0.00");
        defenceUI.text = PlayerManager.acc.livePlayerStats.defense.ToString("0.00");
        agilityUI.text = PlayerManager.acc.livePlayerStats.agility.ToString("0.00");

       float atkSpeed = PlayerManager.acc.basePlayerStats.baseAtkSpeed + PlayerManager.acc.livePlayerStats.attackSpeed / 100;

       dpsUI.text = (PlayerManager.acc.livePlayerStats.attackDamage * atkSpeed).ToString("0.00");
    }
}
