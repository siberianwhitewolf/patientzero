using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
    Vector2 JoystickMovement;
    Vector2 KeyboardMovement;
    int gridPosition = 0;
    int combGridNumber = 0;
    int menuPosition = 0;
    bool openMenuList = false;
    public Animator statusAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                if (openMenuList)
                {
                    CloseMenuList();
                }

                Resume();
            }

            else
            {
                
                gridPosition = 0;
                //moveSelector();
                Pause();
                CheckIfItem();

                if (FindObjectOfType<Weapon>().isEquiped)
                {
                    FindObjectOfType<WeaponUI>().updateUI();
                }
            }
        }



        if (Input.GetButtonDown("Horizontal") && GameIsPaused)
        {
            getControllerInput();
            CheckIfItem();

            if (!openMenuList)
            {
                moveSelector();
            }

            else if (animator.GetBool("Combination") == true)
            {
                moveCombSelector();
            }
        }

        else if (Input.GetButtonDown("Vertical") && GameIsPaused)
        {

            getControllerInput();
            CheckIfItem();

            if (openMenuList && animator.GetBool("Combination") == false)
            {
                moveItemList();
            }

            else if (animator.GetBool("Combination") == true)
            {
                moveCombSelector();
            }

            else
            {
                moveSelector();
            }
        }

        if (Input.GetButtonDown("Action") && GameIsPaused)
        {
            if (gridPosition < 0)
            {
                Resume();
            }

            else if (gridPosition >= 0 && FindObjectOfType<playerStats>().inventory.Container.Count > gridPosition)
            {
                FindObjectOfType<AudioManager>().Play("select-option");
                Item item = FindObjectOfType<playerStats>().inventory.Container[gridPosition].item;

                if (item && item.type != ItemType.Null)
                {
                    {
                        if (!openMenuList)
                        {
                            OpenMenuList(item);
                        }

                        else if (menuPosition == 0 && openMenuList && (item is ConsumableItem))
                        {
                            ConsumableItem itemSelected = (ConsumableItem)item;

                            if (itemSelected.isConsumable)
                            {
                                itemSelected.Use();


                                if (FindObjectOfType<playerStats>().inventory.Container[gridPosition].amount > 1)
                                {
                                    FindObjectOfType<playerStats>().inventory.Container[gridPosition].SubstractAmount(1);
                                }

                                else
                                {
                                    FindObjectOfType<playerStats>().inventory.Container[gridPosition].ID = 0;
                                    FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[gridPosition]);
                                }

                                CloseMenuList();
                            }


                            else
                            {
                                Debug.Log("Este item no es consumible.");
                            }
                        }


                        else if (menuPosition == 0 && openMenuList && (item.type == ItemType.Weapon))
                        {
                            item.Use();
                            FindObjectOfType<Weapon>().weaponEquipped = (WeaponItem)item;
                            WeaponItem weapon = (WeaponItem)item;
                            weapon.gunIsEquipped = true;

                        }


                        else if (menuPosition == 1 && openMenuList)
                        {
                            item = FindObjectOfType<playerStats>().inventory.Container[gridPosition].item;
                            if (item)
                            {
                                FindObjectOfType<dialogBoxManager>().setDescription(item.description);
                                FindObjectOfType<dialogBoxManager>().itemImage.enabled = true;
                                FindObjectOfType<dialogBoxManager>().setImage(item.icon);
                            }

                        }


                        else if (menuPosition == 2 && openMenuList && !animator.GetBool("Combination"))
                        {

                            animator.SetBool("Combination", true);


                            if (gridPosition % 2 == 0 || gridPosition == 0)
                            {
                                combGridNumber = gridPosition + 1;
                                moveCombSelector();
                            }

                            else
                            {
                                combGridNumber = gridPosition - 1;
                                moveCombSelector();
                            }
                        }

                        else if (menuPosition == 2 && openMenuList && animator.GetBool("Combination"))
                        {
                            Item itemToCombine = FindObjectOfType<playerStats>().inventory.Container[combGridNumber].item;

                            if (itemToCombine.type == ItemType.Consumable && item.type != ItemType.Weapon)
                            {
                                ConsumableItem itemSelected = (ConsumableItem)itemToCombine;
                                if (itemSelected.isCombinable)
                                {
                                    int combNumber = itemSelected.Combine(FindObjectOfType<playerStats>().inventory.Container[gridPosition].item);
                                    Debug.Log("Comb number es:" + combNumber);
                                    Debug.Log("ItemSelected es:" + itemSelected.ID);
                                    Debug.Log("Item.ID es:" + item.ID);
                                    if (combNumber == -1)
                                    {
                                        Debug.Log("No se puede combinar este objeto");
                                    }

                                    else if (combNumber == 0 && (itemSelected.ID == 7 || item.ID == 7))
                                    {
                                        if (itemSelected.ID == 7)
                                        {
                                            Debug.Log("Entre por 1");
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[gridPosition]);
                                            FindObjectOfType<WeaponUI>().UpdateText(itemSelected, gridPosition);
                                            CloseMenuList();
                                        }

                                        else
                                        {

                                            Debug.Log("Entre por 2");
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[combGridNumber]);
                                            FindObjectOfType<WeaponUI>().UpdateText(item, combGridNumber);
                                            CloseMenuList();
                                        }

                                        if (openMenuList)
                                        {
                                            CloseMenuList();
                                            if (animator.GetBool("Combination") == true)
                                            {
                                                animator.SetBool("Combination", false);
                                                combGridNumber = 0;
                                            }
                                        }
                                    }

                                    else if (item.ID == 1)
                                    {
                                        if (FindObjectOfType<playerStats>().inventory.Container[gridPosition].amount > 1)
                                        {
                                            Debug.Log("Combinando hierba verde con tubo de ensayo cuando tengo mas que 1");
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].SubstractAmount(1);
                                            FindObjectOfType<WeaponUI>().UpdateText(item, gridPosition);
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].UpdateItemID(combNumber);
                                        }


                                        else
                                        {
                                            Debug.Log("Combinando hierba verde con tubo de ensayo cuando no tengo mas que 1");
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[gridPosition]);
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].UpdateItemID(combNumber);
                                        }

                                        CloseMenuList();
                                        if (animator.GetBool("Combination") == true)
                                        {
                                            animator.SetBool("Combination", false);
                                            combGridNumber = 0;
                                        }
                                    }

                                    else if (item.type != ItemType.Weapon)
                                    {

                                        if (FindObjectOfType<playerStats>().inventory.Container[combGridNumber].amount > 1 && FindObjectOfType<playerStats>().inventory.Container[combGridNumber].ID == 1)
                                        {
                                            Debug.Log("Combinando tubo de ensayo con hierba verde cuando tengo mas que 1");
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].SubstractAmount(1);
                                            FindObjectOfType<WeaponUI>().UpdateText(item, combGridNumber);
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].UpdateItemID(combNumber);
                                        }

                                        else
                                        {
                                            Debug.Log("Combinando tubo de ensayo con hierba verde cuando no tengo mas que 1");
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[combGridNumber]);
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].UpdateItemID(combNumber);
                                        }

                                        CloseMenuList();
                                        if (animator.GetBool("Combination") == true)
                                        {
                                            animator.SetBool("Combination", false);
                                            combGridNumber = 0;
                                        }
                                    }

                                }
                            }

                            else if (item.type == ItemType.Consumable)
                            {
                                ConsumableItem itemSelected = (ConsumableItem)item;
                                if (itemSelected.isCombinable)
                                {
                                    int combNumber = itemSelected.Combine(FindObjectOfType<playerStats>().inventory.Container[combGridNumber].item);
                                    Debug.Log(combNumber);
                                    Debug.Log(itemSelected.name);
                                    Debug.Log(itemToCombine.name);
                                    if (combNumber == -1)
                                    {
                                        Debug.Log("No se puede combinar este objeto");
                                    }

                                    else if (combNumber == 0 && (itemSelected.ID == 7 || itemToCombine.ID == 7))
                                    {

                                        if (itemSelected.ID == 7)
                                        {
                                            Debug.Log("Entre por 4");
                                            FindObjectOfType<playerStats>().inventory.Container[gridPosition].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[gridPosition]);
                                            FindObjectOfType<WeaponUI>().UpdateText(item, gridPosition);
                                            CloseMenuList();
                                        }

                                        else
                                        {

                                            Debug.Log("Entre por 5");
                                            FindObjectOfType<playerStats>().inventory.Container[combGridNumber].ID = 0;
                                            FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[combGridNumber]);
                                            FindObjectOfType<WeaponUI>().UpdateText(itemToCombine, combGridNumber);
                                            CloseMenuList();
                                        }
                                    }

                                    else
                                    {
                                        Debug.Log("Entre por 6");
                                        FindObjectOfType<WeaponUI>().UpdateText(item, gridPosition);

                                        CloseMenuList();
                                        if (animator.GetBool("Combination") == true)
                                        {
                                            animator.SetBool("Combination", false);
                                            combGridNumber = 0;
                                        }
                                    }
                                }
                            }
                            else if (item.type == ItemType.Weapon)
                            {
                                ConsumableItem consumableItem = (ConsumableItem)itemToCombine;
                                if (consumableItem.isCombinable)
                                {
                                    int combNumber = consumableItem.Combine(FindObjectOfType<playerStats>().inventory.Container[gridPosition].item);
                                    Debug.Log(combNumber);
                                    Debug.Log(item.name);
                                    Debug.Log(consumableItem.name);
                                    if (combNumber == -1)
                                    {
                                        Debug.Log("No se puede combinar este objeto");
                                    }

                                    else if (combNumber == 0 && (item.ID == 7 || consumableItem.ID == 7))
                                    {
                                        Debug.Log("Entre por 4");
                                        FindObjectOfType<playerStats>().inventory.Container[combGridNumber].ID = 0;
                                        FindObjectOfType<playerStats>().inventory.RemoveItem(FindObjectOfType<playerStats>().inventory.Container[combGridNumber]);
                                        FindObjectOfType<WeaponUI>().UpdateText(consumableItem, combGridNumber);
                                        CloseMenuList();
                                    }

                                }
                            }
                        }
                    }
                }
            }

            else
            {
                Debug.Log("You currently don't have any items in this slot");
            }
         }

            else if (Input.GetButtonDown("Cancel") && GameIsPaused)
            {
                if (openMenuList)
                {
                    CloseMenuList();
                    if (animator.GetBool("Combination") == true)
                    {
                        animator.SetBool("Combination", false);
                        combGridNumber = 0;
                    }
                }

                else
                {
                    Resume();
                }

            }

            chechkIfCanExit();

        }

        void OpenMenuList(Item item)
        {
            if (item != null)
            {
                //ABRIR PANEL DE OPCIONES DEL ITEM
                if (!openMenuList && gridPosition >= 0)
                {
                    menuPosition = 0;
                    moveItemList();
                    openMenuList = true;
                    animator.SetBool("Menu", true);
                }
            }
        }

        void CloseMenuList()
        {
            {
                //CERRAR PANEL DE OPCIONES DEL ITEM
                if (openMenuList && gridPosition >= 0)
                {
                    menuPosition = 0;
                    moveItemList();
                    openMenuList = false;
                    animator.SetBool("Menu", false);
                    // Limpio el dialog box manager
                    FindObjectOfType<dialogBoxManager>().clearDescription();
                    FindObjectOfType<dialogBoxManager>().itemImage.enabled = false;
                    FindObjectOfType<dialogBoxManager>().clearImage();
                }
            }
        }


        void Resume()
        {
            animator.SetBool("Exit", true);
            FindObjectOfType<AudioManager>().Play("open-inventory");
        }



        void Pause()
        {

            FindObjectOfType<AudioManager>().Play("open-inventory");
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            // asigno al animator de la UI el valor de si tengo un arma o no equipada del arma.
            FindObjectOfType<WeaponUI>().animator.SetBool("gunIsEquipped", FindObjectOfType<Weapon>().isEquiped);
            statusAnimator.SetInteger("playerHealth", FindObjectOfType<playerStats>().currentHealth);

    }


        void getControllerInput()
        {
            JoystickMovement.x = Input.GetAxisRaw("Move Horizontal");
            JoystickMovement.y = Input.GetAxisRaw("Move Vertical");

            KeyboardMovement.x = Input.GetAxisRaw("Horizontal");
            KeyboardMovement.y = Input.GetAxisRaw("Vertical");

            if (JoystickMovement != Vector2.zero && GameIsPaused)
            {
                if (JoystickMovement.x == 1 && openMenuList == false)
                {
                    gridPosition++;
                }

                else if (JoystickMovement.x == -1 && openMenuList == false)
                {
                    gridPosition--;
                }

                if (JoystickMovement.y == 1 && openMenuList == false)
                {
                    gridPosition = gridPosition + 2;
                }

                else if (JoystickMovement.y == -1 && openMenuList == false)
                {
                    gridPosition = gridPosition - 2;
                }

                CheckIfGridPositionIsOutOfBounds();
            }

            else if (KeyboardMovement != Vector2.zero && GameIsPaused)
            {
                //Movimiento de selector de inventario

                if (KeyboardMovement.x == 1 && openMenuList == false && !animator.GetBool("Combination"))
                {

                    gridPosition++;

                }

                else if (KeyboardMovement.x == -1 && openMenuList == false && !animator.GetBool("Combination"))
                {
                    gridPosition--;
                }

                if (KeyboardMovement.y == -1 && openMenuList == false && !animator.GetBool("Combination"))
                {
                    gridPosition = gridPosition + 2;
                }

                else if (KeyboardMovement.y == 1 && openMenuList == false && !animator.GetBool("Combination"))
                {
                    gridPosition = gridPosition - 2;
                }

                //Movimiento de lista de menu

                if (KeyboardMovement.y == -1 && openMenuList == true && !animator.GetBool("Combination"))
                {
                    menuPosition++;
                }

                else if (KeyboardMovement.y == 1 && openMenuList == true && !animator.GetBool("Combination"))
                {
                    menuPosition--;
                }

                if (openMenuList)
                {
                    CheckIfMenuPositionIsOutOfBounds();
                    moveItemList();
                }



                else
                {
                    CheckIfGridPositionIsOutOfBounds();
                    moveSelector();
                }

                //Movimiento de grilla de Comb
                if (animator.GetBool("Combination"))
                {
                    if (KeyboardMovement.x == 1)
                    {

                        combGridNumber++;

                    }

                    else if (KeyboardMovement.x == -1)
                    {
                        combGridNumber--;
                    }

                    if (KeyboardMovement.y == -1)
                    {
                        combGridNumber = combGridNumber + 2;
                    }

                    else if (KeyboardMovement.y == 1)
                    {
                        combGridNumber = combGridNumber - 2;
                    }

                    CheckIfCombGridNumberIsEqualsToGridNumber();
                    CheckIfCombGridNumberIsOutOfBounds();
                    moveCombSelector();
                }
            }
        }

        void CheckIfItem()
        {
            if (GameIsPaused)
            {
                InventoryObject inventory = FindObjectOfType<playerStats>().inventory;

                if (inventory.Container.Count > 0)
                {
                    if (inventory.Container.Count - 1 >= gridPosition && gridPosition >= 0)
                    {
                        Item item = inventory.Container[gridPosition].item;

                        if (item && item.type != ItemType.Null)
                        {
                            FindObjectOfType<dialogBoxManager>().setDescription(item.name);
                            FindObjectOfType<dialogBoxManager>().setImage(item.icon);
                        }

                        else
                        {
                            FindObjectOfType<dialogBoxManager>().clearDescription();
                            FindObjectOfType<dialogBoxManager>().itemImage.enabled = false;
                        }
                    }

                    else
                    {
                        FindObjectOfType<dialogBoxManager>().clearDescription();
                        FindObjectOfType<dialogBoxManager>().itemImage.enabled = false;
                    }
                }
            }
        }

        void CheckIfGridPositionIsOutOfBounds()
        {
            if (gridPosition < -2)
            {
                gridPosition = 7;
            }

            else if (gridPosition > 7)
            {
                gridPosition = 0;
            }
        }


        void CheckIfCombGridNumberIsOutOfBounds()
        {
            if (combGridNumber < 0 && combGridNumber % 2 == 0)
            {
                combGridNumber = 6;
            }

            else if (combGridNumber < 0 && !(combGridNumber % 2 == 0))
            {
                combGridNumber = 7;
            }


            if (combGridNumber > 7 && combGridNumber % 2 == 0)
            {
                combGridNumber = 0;
            }

            else if (combGridNumber > 7 && !(combGridNumber % 2 == 0))
            {
                combGridNumber = 1;
            }
        }

        void CheckIfCombGridNumberIsEqualsToGridNumber()
        {

            if (combGridNumber == gridPosition)
            {
                combGridNumber = gridPosition - 2;
            }

        }

        void CheckIfMenuPositionIsOutOfBounds()
        {

            if (menuPosition < 0)
            {
                menuPosition = 2;
            }

            else if (menuPosition > 2)
            {
                menuPosition = 0;
            }

        }


        void moveSelector()
        {
            animator.SetInteger("gridNumber", gridPosition);
        FindObjectOfType<AudioManager>().Play("menu-selector");
        }

        void moveCombSelector()
        {
            animator.SetInteger("combGridNumber", combGridNumber);
            FindObjectOfType<AudioManager>().Play("menu-selector");
    }


        void moveItemList()
        {
            animator.SetInteger("MenuNumber", menuPosition);
            FindObjectOfType<AudioManager>().Play("menu-selector");
    }


        void chechkIfCanExit()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("NoneSelected"))
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
        }
    }