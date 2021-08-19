using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogManager : MonoBehaviour


{
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public Animator animator;
    public Image imagePortrait;
    private Story story;
    private bool choicesDisplayed;
    private InteractableObject interactable;
    private List<string> tags;
    public Image AButton;
    public Image BButton;
    public static bool dialogueIsBeingTyped = false;
    public int optionNumber = 0;

    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("DialogBoxIsOpen", true);
        imagePortrait.sprite = dialog.portrait;
        story = new Story(dialog.inkJSON.text);
        story.EvaluateFunction("asignar_contiene_item", interactable.GetComponent<InteractableObject>().contains_item);
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (story.canContinue)
        {
            string sentence = "";
            sentence = story.Continue();
            ParseTags();
            StopAllCoroutines();
            if(sentence == "")
            {
                EndDialog();
            }
            else
            {
                StartCoroutine(TypeSentence(sentence));
            }
        }

        else
        {
            if (CheckIfHasChoices() && !choicesDisplayed)
            {
                DisplayChoices();
            }

            else if (choicesDisplayed)
            {
               

                if (Input.GetButtonDown("Action") && optionNumber == 0)
                {
                    Debug.Log("Action button pressed");
                    story.ChooseChoiceIndex(0);
                    HideChoices();
                    DisplayNextSentence();
                }

                else if (Input.GetButtonDown("Action") && optionNumber == 1)
                {
                    Debug.Log("Cancel button pressed");
                    story.ChooseChoiceIndex(1);
                    HideChoices();
                    DisplayNextSentence();
                }
            }

            else
            {
                EndDialog();
                return;
            }
        }

        IEnumerator TypeSentence(string sentence)
        {
            textMeshPro.text = "";
            dialogueIsBeingTyped = true;
            foreach (char letter in sentence.ToCharArray())
            {
                textMeshPro.text += letter;

                yield return new WaitForSeconds(0.01f);
            }

            dialogueIsBeingTyped = false;

        }
    }

    void EndDialog()
    {
        animator.SetBool("DialogBoxIsOpen", false);
        if(interactable.name == "gun" && optionNumber == 0|| interactable.name == "herb" && optionNumber == 0 || interactable.name == "9mm Bullets" && optionNumber == 0)
        {
            interactable.wasDestroyed = 1;
            interactable.saveInteractableData();
            Destroy(interactable.gameObject);
        }

        // Le asigno a contains_item el valor de la variable declarada dentro del archivo de Ink asociado
        // con el objeto interactuable.

        if (interactable)
        {
            interactable.GetComponent<InteractableObject>().contains_item = (int)story.variablesState["contiene_item"];
        }

        // Vuelvo a 0 la variable option number para que la proxima vez que interactue con un
        // interactable object no este modificada la posicion del cursor.

        optionNumber = 0;

        return;
    }

    bool CheckIfHasChoices()
    {

        if (story.currentChoices.Count > 0)
        {
            return true;
        }

        return false;
    }

    void DisplayChoices()
    {
        textMeshPro.text = "";
        choice1.text = story.currentChoices[0].text;
        choice2.text = story.currentChoices[1].text;
        AButton.enabled = true;
        BButton.enabled = false;
        choicesDisplayed = true;
    }

    void HideChoices()
    {
        textMeshPro.text = "";
        choice1.text = "";
        choice2.text = "";
        AButton.enabled = false;
        BButton.enabled = false;
        choicesDisplayed = false;
    }

    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string tag in tags)
        {
            string prefix = tag.Split(' ')[0];

            switch (prefix.ToLower())
            {

                case "hierba":
                    FindObjectOfType<playerStats>().inventory.AddItem(interactable.item, 1);
                    interactable.contains_item = 0;
                    interactable.saveInteractableData();
                    break;

                case "alice":
                    FindObjectOfType<playerStats>().inventory.AddItem(interactable.item, 1);
                    interactable.contains_item = 0;
                    interactable.saveInteractableData();
                    break;

                case "tienellave":

                    for (int i = 0; i < FindObjectOfType<playerStats>().inventory.Container.Count; i++)
                    {
                        if(FindObjectOfType<playerStats>().inventory.Container[i].item != null)
                        {
                            if (FindObjectOfType<playerStats>().inventory.Container[i].item.type == ItemType.Key)
                            {
                                story.EvaluateFunction("asignar_contiene_item", 1);
                            }
                        }
                        
                    }

                    break;

                case "llave":
                    FindObjectOfType<playerStats>().inventory.AddItem(interactable.item, 1);
                    interactable.contains_item = 0;
                    break;

                case "abrirpuerta":
                    int index = FindObjectOfType<LevelLoader>().sceneIndex = 4;
                    FindObjectOfType<LevelLoader>().ChangeScene(index);
                    break;


                case "tubo":
                    FindObjectOfType<playerStats>().inventory.AddItem(interactable.item, 1);
                    interactable.contains_item = 0;
                    interactable.saveInteractableData();
                    break;

                case "balaspistola":
                    FindObjectOfType<playerStats>().inventory.AddItem(interactable.item, 1);
                    interactable.contains_item = 0;
                    interactable.saveInteractableData();
                    break;

                default:
                    break;
            }
        }  
    }


    private void Update()
    {

        // Para seleccionar la opcion que quiero

        if (choicesDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (optionNumber < 1)
                {
                    optionNumber++;
                }
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                if (optionNumber > 0)
                {
                    optionNumber--;
                }
            }

            if (optionNumber == 0)
            {
                AButton.enabled = true;
                BButton.enabled = false;
            }

            else
            {
                AButton.enabled = false;
                BButton.enabled = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<InteractableObject>();
    }

}
