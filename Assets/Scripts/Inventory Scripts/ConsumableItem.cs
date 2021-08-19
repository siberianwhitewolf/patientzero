using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class ConsumableItem : Item
{
    public bool isCombinable;
    public bool isConsumable;
    public int restoreHealthValue;
    public int maxRestoreHealthValue;
    private int acumRestore;

    private void Awake()
    {
        type = ItemType.Consumable;
        if (this.name == "Balas 9mm")
        {
            restoreHealthValue = maxRestoreHealthValue;
        }
    }


    public override void Use()
    {
        if (this.isConsumable)
        {
            switch (this.name)
            {

                case "Hierba Verde":
                    FindObjectOfType<playerStats>().Heal(restoreHealthValue);
                    break;

                case "Mezcla 2 hierbas":
                    FindObjectOfType<playerStats>().Heal(restoreHealthValue);
                    break;

                case "Mezcla de hierbas":
                    FindObjectOfType<playerStats>().Heal(restoreHealthValue);
                    break;



                default:

                    break;
            }
        }   
    }


    public int Combine(Item item)
    {
        switch (item.name)
        {

            case "Hierba Verde":

                if (this.name == "Tubo de ensayo")
                {
                    return 5;
                }

                else if (this.name == "Mezcla de hierbas")
                {
                    return 6;
                }

                break;

            case "Tubo de ensayo":

                if (this.name == "Hierba Verde")
                {
                    return 5;
                }

                break;

            case "Mezcla de hierbas":

                if (this.name == "Hierba Verde")
                {
                    return 6;
                }

                break;

            case "Alice":

                if (this.name == "Balas 9mm")
                {
                    int auxHealthValue = restoreHealthValue;

                    restoreHealthValue = FindObjectOfType<Weapon>().Reload(restoreHealthValue);

                    acumRestore += auxHealthValue - restoreHealthValue;

                    if(acumRestore >= maxRestoreHealthValue)
                    {
                        acumRestore -= maxRestoreHealthValue;
                        for (int i = 0; i < FindObjectOfType<playerStats>().inventory.Container.Count; i++)
                        {
                            if (FindObjectOfType<playerStats>().inventory.Container[i].ID == 7)
                            {
                                FindObjectOfType<playerStats>().inventory.Container[i].SubstractAmount(1);
                            }
                        }
                    }

                    if (restoreHealthValue == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return restoreHealthValue;
                    }
                    
                }

                break;

            case "Balas 9mm":

                if (this.name == "Alice")
                {
                    int auxHealthValue = restoreHealthValue;

                    restoreHealthValue = FindObjectOfType<Weapon>().Reload(restoreHealthValue);
                    acumRestore += auxHealthValue - restoreHealthValue;

                    if (acumRestore >= maxRestoreHealthValue)
                    {
                        acumRestore -= maxRestoreHealthValue;
                        for (int i = 0; i < FindObjectOfType<playerStats>().inventory.Container.Count; i++)
                        {
                            if (FindObjectOfType<playerStats>().inventory.Container[i].ID == 7)
                            {
                                FindObjectOfType<playerStats>().inventory.Container[i].SubstractAmount(1);
                            }
                        }
                    }
                    if (restoreHealthValue == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return restoreHealthValue;
                    }
                }

                break;

            default:
                return -1;
        }

        return -1;
    }


}
