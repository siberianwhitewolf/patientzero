using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerStats>().savePlayerData();

            collision.gameObject.GetComponent<playerStats>().inventory.Save();
            if (FindObjectOfType<Weapon>().isEquiped)
            {
                FindObjectOfType<Weapon>().weaponEquipped.firstTimeEquiped = true;
                FindObjectOfType<Weapon>().weaponEquipped.gunIsEquipped = true;
            }

            ChangeScene(sceneIndex);
        }
    }

  public void ChangeScene(int index)
    {

        StartCoroutine(LoadLevel(index));
       
    }



    IEnumerator LoadLevel(int levelIndex)
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        if (FindObjectOfType<playerStats>()) { 
        FindObjectOfType<playerStats>().loadPlayerData();
        FindObjectOfType<playerStats>().inventory.Load();
        }
    }

}
