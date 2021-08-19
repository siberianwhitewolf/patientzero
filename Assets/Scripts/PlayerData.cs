using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerCurrentStatus;
    public int[] ItemsID;
    public int currentHealth;


    public PlayerData(playerStats stats)
    {
        playerCurrentStatus = stats.playerCurrentStatus;
        currentHealth = stats.currentHealth;
    }
}
