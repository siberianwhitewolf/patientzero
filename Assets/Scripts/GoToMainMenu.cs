using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<LevelLoader>().ChangeScene(FindObjectOfType<LevelLoader>().sceneIndex);
        }

    }


    public void GoToNextLevel()
    {
        FindObjectOfType<LevelLoader>().ChangeScene(FindObjectOfType<LevelLoader>().sceneIndex);     
    }


}
