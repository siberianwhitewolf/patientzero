using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void SaveData()
    {
        SaveSystem.SavePlayer(FindObjectOfType<playerStats>());
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        FindObjectOfType<playerStats>().playerCurrentStatus = data.playerCurrentStatus;
        FindObjectOfType<playerStats>().currentHealth = data.currentHealth;
        
    }
}
