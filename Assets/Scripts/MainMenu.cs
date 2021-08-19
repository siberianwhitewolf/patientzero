using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Animator menuAnimator;
    int menuPosition = 0;
    Vector2 JoystickMovement;
    Vector2 KeyboardMovement;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause") && menuAnimator.GetBool("Press Start"))
        {
            menuAnimator.SetBool("Press Start", false);
            menuPosition = 1;
            moveSelector();
        }


        
        if (Input.GetButtonDown("Horizontal") && !menuAnimator.GetBool("Press Start"))
        {

            getControllerInput();
            moveSelector();

        }

        else if (Input.GetButtonDown("Vertical") && !menuAnimator.GetBool("Press Start"))
        {
            getControllerInput();
            moveSelector();
        }
        

        if (Input.GetButtonDown("Action") && !menuAnimator.GetBool("Press Start"))
        {
            StartCoroutine(WaitForTransition());

            switch (menuPosition)
            {
                case 1:
                    menuAnimator.SetBool("start",true);
                    int index = FindObjectOfType<LevelLoader>().sceneIndex = 1;
                    FindObjectOfType<LevelLoader>().ChangeScene(index);
                    break;

                case 2:
                    menuAnimator.SetBool("credits",true);

                    break;

                case 3:
                    menuAnimator.SetBool("controls",true);
                    break;

                case 0:
                    break;

            }

        }

        else if (Input.GetButtonDown("Cancel") && !menuAnimator.GetBool("Press Start"))
        {
            switch (menuPosition)
            {
                
                case 2:
                    if (menuAnimator.GetBool("credits"))
                    {
                        menuAnimator.SetBool("credits", false);
                    }
                    break;

                case 3:
                    if (menuAnimator.GetBool("controls"))
                    {
                        menuAnimator.SetBool("controls", false);
                    }
                    break;

                case 0:
                    break;

            }

        }


        IEnumerator WaitForTransition()
        {
            yield return new WaitForSeconds(1f);
            menuAnimator.SetTrigger("transition");
        }

        void getControllerInput()
        {
            JoystickMovement.x = Input.GetAxisRaw("Move Horizontal");
            JoystickMovement.y = Input.GetAxisRaw("Move Vertical");

            KeyboardMovement.x = Input.GetAxisRaw("Horizontal");
            KeyboardMovement.y = Input.GetAxisRaw("Vertical");

            if (JoystickMovement != Vector2.zero)
            {

                if (JoystickMovement.y == 1)
                {
                    menuPosition++;
                }

                else if (JoystickMovement.y == -1)
                {
                    menuPosition--;
                }

                CheckIfGridPositionIsOutOfBounds();
            }

            else if (KeyboardMovement != Vector2.zero)
            {
               

                if (KeyboardMovement.y == -1)
                {
                    menuPosition++;
                }

                else if (KeyboardMovement.y == 1)
                {
                    menuPosition--;
                }

                CheckIfGridPositionIsOutOfBounds();

            }

            void CheckIfGridPositionIsOutOfBounds()
            {
                if (menuPosition < 1)
                {
                    menuPosition = 1;
                }

                else if (menuPosition > 3)
                {
                    menuPosition = 3;
                }
            }
        }


        void moveSelector()
        {
            menuAnimator.SetInteger("menu", menuPosition);
        }

    }
}

