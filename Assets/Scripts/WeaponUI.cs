using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI WeaponSlotText;
    public TextMeshProUGUI Slot0Text;
    public TextMeshProUGUI Slot1Text;
    public TextMeshProUGUI Slot2Text;
    public TextMeshProUGUI Slot3Text;
    public TextMeshProUGUI Slot4Text;
    public TextMeshProUGUI Slot5Text;
    public TextMeshProUGUI Slot6Text;
    public TextMeshProUGUI Slot7Text;
    public WeaponItem weapon;
    public Image image;
    public int GunSlot;
    public int AmmoSlot;

    private void Awake()
    {
        InventoryObject inventory = FindObjectOfType<playerStats>().inventory;
        for (int i = 0; i < inventory.Container.Count; i++)
        {

            if (inventory.Container[i].item.type == ItemType.Weapon)
            {
                GunSlot = i;
                WeaponItem weapon = (WeaponItem)inventory.Container[i].item;
                if (weapon.gunIsEquipped && !weapon.gunWasEquipped)
                {
                    this.weapon = weapon;
                    weapon.gunIsEquipped = false;
                    weapon.Use();

                }
            }


            if (inventory.Container[i].item.ID == 7)
            {
                ConsumableItem ammoBox = (ConsumableItem)inventory.Container[i].item;
                AmmoSlot = i;

                switch (AmmoSlot)
                {
                    case 0:
                        Slot0Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 1:
                        Slot1Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 2:
                        Slot2Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 3:
                        Slot3Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 4:
                        Slot4Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 5:
                        Slot5Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 6:
                        Slot6Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    case 7:
                        Slot7Text.text = ammoBox.restoreHealthValue.ToString();
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void updateUI()
    {
        FindObjectOfType<Weapon>().magazineSize = weapon.maxBulletsInMagazine;
        FindObjectOfType<Weapon>().currentBulletsInMagazine = weapon.currentBulletsInMagazine;
        weapon.firstTimeEquiped = false;
        UpdateText(weapon, GunSlot);
        WeaponSlotText.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
        image.sprite = weapon.icon;
        UpdateText(weapon, GunSlot);
    }


    public void UpdateText(Item item, int position)
    {
        if(item.type == ItemType.Weapon)
        {
            switch (position)
            {
                case 0:
                    Slot0Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 1:
                    Slot1Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;
                    
                case 2:
                    Slot2Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 3:
                    Slot3Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 4:
                    Slot4Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 5:
                    Slot5Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 6:
                    Slot6Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;

                case 7:
                    Slot7Text.text = FindObjectOfType<Weapon>().currentBulletsInMagazine.ToString();
                    break;


                default:
                    break;
            }
        }

        else {

            ConsumableItem ammoBox = (ConsumableItem)item;

            

            switch (position)
            {
                case 0:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot0Text.text = "";
                    }

                    else
                    {
                        Slot0Text.text = ammoBox.restoreHealthValue.ToString();
                    }
                    
                    break;

                case 1:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot1Text.text = "";
                    }

                    else
                    {
                        Slot1Text.text = ammoBox.restoreHealthValue.ToString();
                    }
                    break;

                case 2:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot2Text.text = ammoBox.restoreHealthValue.ToString();
                    }

                    else
                    {
                        Slot2Text.text = ammoBox.restoreHealthValue.ToString();
                    }

                    break;

                case 3:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot3Text.text = "";
                    }

                    else
                    {
                        Slot3Text.text = ammoBox.restoreHealthValue.ToString();
                    }

                    break;

                case 4:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot4Text.text = "";
                    }

                    else
                    {
                        Slot4Text.text = ammoBox.restoreHealthValue.ToString();
                    }
                    break;

                case 5:

                    if (ammoBox.restoreHealthValue == 0)

                    {
                        Slot5Text.text = "";
                    }

                    else
                    {
                        Slot5Text.text = ammoBox.restoreHealthValue.ToString();
                    }
                    
                    break;

                case 6:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot6Text.text = "";
                    }

                    else
                    {
                     Slot6Text.text = ammoBox.restoreHealthValue.ToString();
                    }

                    break;

                case 7:

                    if (ammoBox.restoreHealthValue == 0)
                    {
                        Slot7Text.text = "";
                    }

                    else
                    {
                        Slot7Text.text = ammoBox.restoreHealthValue.ToString();
                    }

                    break;

                default:
                    break;
            }
        }

    }

    public void UpdateItem(int position)
    {
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ID == position)
            {
                Debug.Log("ENTRE");
                Debug.Log(slots[i].item.name);
                Debug.Log(FindObjectOfType<playerStats>().inventory.Container[position].item.name);
                slots[i].item = FindObjectOfType<playerStats>().inventory.Container[position].item;
            }
        }
    }



}
