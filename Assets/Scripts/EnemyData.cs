using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int health;
    public bool isDead;


    public EnemyData(Enemy enemy)
    {
        health = enemy.health;
        isDead = enemy.isDead;
    }

}
