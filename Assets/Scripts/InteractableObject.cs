using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InteractableObject : MonoBehaviour
{
    
    public GameObject dialogBox;
    public Dialog dialog;
    public bool playerInRange;
    public Transform firepoint;
    // La variable contains item sirve para verificar si el objeto interactuable
    // Contiene o no un item que pueda ser obtenido por el jugador validado por el archivo
    // de inky.
    // . 0 es falso, 1 es verdadero.
    public int contains_item = 0;
    public Item item;
    public int wasDestroyed = 0;
    public int uniqueID;


    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
       saveInteractableData();
        if(wasDestroyed == 1)
        {
            Destroy(this.gameObject);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action") && playerInRange && !PauseMenu.GameIsPaused)
        {

            if (FindObjectOfType<DialogManager>().animator.GetBool("DialogBoxIsOpen") == true)
            {
                if (!DialogManager.dialogueIsBeingTyped)
                {
                    FindObjectOfType<DialogManager>().DisplayNextSentence();
                }
            }

            else
            {
                FindObjectOfType<DialogManager>().StartDialog(dialog);
            }
            }


        }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            ShowButtonIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            HideButtonIcon();
        }
    }

    private void ShowButtonIcon()
    {
        firepoint.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void HideButtonIcon()
    {
        firepoint.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDestroy()
    {
      
    }

    private void OnApplicationQuit()
    {
        wasDestroyed = 0;
    }

    public void saveInteractableData()
    {
        SaveSystem.SaveInteractable(this, uniqueID);
    }

    public void loadInteractableData()
    {
        InteractableData interactableData = SaveSystem.LoadInteractable(this.uniqueID);
        if (interactableData != null)
        {
            wasDestroyed = interactableData.wasDestroyed;
            contains_item = interactableData.contains_item;

        }
    }

}
