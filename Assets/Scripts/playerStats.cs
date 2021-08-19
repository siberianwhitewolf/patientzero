using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class playerStats : MonoBehaviour
{

    private enum Status
    {Bien,
    Cuidado,
    Peligro,
    Veneno,
    Hemorragia,
    Infectado,
    Parálisis,
    Muerto
    };

    public string playerCurrentStatus = Status.Bien.ToString();
    public InventoryObject inventory;
    public int currentHealth;
    public int MaxHealth = 10;
    public Animator statusAnimator;

    private void Awake()
    {

       savePlayerData();

        if(inventory.Container.Count < 1)
        {
            inventory.CreateInventory();
        }

    }


    private void Start()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            //Si tengo el arma en el inventario y me la habia equipado, la equipo, asi persiste entre escenas.
            if (inventory.Container[i].item.ID == 2)
            {
                WeaponItem weapon = (WeaponItem)inventory.Container[i].item;
                if (weapon.gunIsEquipped)
                {

                    FindObjectOfType<Weapon>().weaponEquipped = weapon;
                    FindObjectOfType<Weapon>().isEquiped = true;
                    FindObjectOfType<Weapon>().magazineSize = weapon.maxBulletsInMagazine;
                    FindObjectOfType<Weapon>().currentBulletsInMagazine = weapon.currentBulletsInMagazine;
                    weapon.firstTimeEquiped = true;
                    weapon.gunWasEquipped = true;

                }
            }
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Die();
        }

    }


    public void Heal(int healingPoints)
    {

        currentHealth += healingPoints;

        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        statusAnimator.SetInteger("playerHealth", FindObjectOfType<playerStats>().currentHealth);
        FindObjectOfType<AudioManager>().Play("heal");
    }


    public void Die ()
    {

        GetComponentInChildren<Animator>().SetTrigger("Die");
        playerCurrentStatus = Status.Muerto.ToString();
        int index = FindObjectOfType<LevelLoader>().sceneIndex = 3;
        FindObjectOfType<LevelLoader>().transitionTime = 3;
        FindObjectOfType<LevelLoader>().ChangeScene(index);

    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        currentHealth = 10;
    }


    public void savePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }


    public void loadPlayerData()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        Debug.Log(playerData);
        if(playerData != null)
        {
            currentHealth = playerData.currentHealth;
            playerCurrentStatus = playerData.playerCurrentStatus;
        }
    }
}
