using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public int health = 5;
    public bool isDead = false;
    public int uniqueID;

    void Start()
    {
       saveEnemyData();
        if (isDead)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        saveEnemyData();
        Destroy(gameObject);
    }

    public void saveEnemyData()
    {
        SaveSystem.SaveEnemy(this, uniqueID);
    }

    public void loadEnemyData()
    {
        EnemyData enemyData = SaveSystem.LoadEnemy(this.uniqueID);
        if (enemyData != null)
        {
            health = enemyData.health;
            isDead = enemyData.isDead;

        }
    }
}
