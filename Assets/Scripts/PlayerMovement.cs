using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public new Rigidbody2D rigidbody2D;
    public GameObject dialogBox;
    Vector2 JoystickMovement;
    [SerializeField]Vector2 KeyboardMovement;
    public Animator animator;


    private void Start()
    {
        animator.SetFloat("Horizontal", KeyboardMovement.x);
        animator.SetFloat("Vertical", KeyboardMovement.y);
    }

    

    void Update()
    {

        // si no estoy en un dialogo, o en pausa y no estoy muerto, entonces me puedo mover

        if (FindObjectOfType<DialogManager>().animator.GetBool("DialogBoxIsOpen") == false && FindObjectOfType<playerStats>().playerCurrentStatus != "Muerto" && !PauseMenu.GameIsPaused)
        {
            // Input
            JoystickMovement.x = Input.GetAxisRaw("Move Horizontal");
            JoystickMovement.y = Input.GetAxisRaw("Move Vertical");

            KeyboardMovement.x = Input.GetAxisRaw("Horizontal");
            KeyboardMovement.y = Input.GetAxisRaw("Vertical");

            if (JoystickMovement != Vector2.zero)
            {
                animator.SetFloat("Horizontal", JoystickMovement.x);
                animator.SetFloat("Vertical", JoystickMovement.y);
                
            }

           else if (KeyboardMovement != Vector2.zero)
            {
                animator.SetFloat("Horizontal", KeyboardMovement.x);
                animator.SetFloat("Vertical", KeyboardMovement.y);
                
            }

            animator.SetFloat("Speed", JoystickMovement.sqrMagnitude);
            animator.SetFloat("Speed", KeyboardMovement.sqrMagnitude);

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FindObjectOfType<playerStats>().TakeDamage(1);
        }

    }

    void FixedUpdate()
    {
        // Movement
        if (JoystickMovement != Vector2.zero)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + JoystickMovement * moveSpeed * Time.fixedDeltaTime);
        }

        else if (KeyboardMovement != Vector2.zero)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + KeyboardMovement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
