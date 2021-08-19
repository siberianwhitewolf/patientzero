using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]

public class WeaponItem : Item
{
    private void Awake()
    {
        type = ItemType.Weapon;
    }

    public int maxBulletsInMagazine;
    public int currentBulletsInMagazine;
    public bool gunIsEquipped = false;
    public bool firstTimeEquiped = true;
    public bool gunWasEquipped = false;

    public override void Use()
    {
        switch (this.name)
        {

            case "Alice":

                gunIsEquipped = FindObjectOfType<WeaponUI>().animator.GetBool("gunIsEquipped");

                if (gunIsEquipped)
                {
                    Unequip();
                }

                else
                {
                    Equip();
                }
                break;

            default:
                break;
        }

    }

    public void Unequip()
    {
        FindObjectOfType<WeaponUI>().animator.SetBool("gunIsEquipped", false);
        FindObjectOfType<WeaponUI>().WeaponSlotText.text = "";
        FindObjectOfType<WeaponUI>().UpdateText(this, FindObjectOfType<WeaponUI>().GunSlot);
        FindObjectOfType<WeaponUI>().weapon = null;
        FindObjectOfType<WeaponUI>().image.sprite = null;
        FindObjectOfType<Weapon>().isEquiped = false;
    }

    public void Equip()
    {
        FindObjectOfType<Weapon>().isEquiped = true;
        FindObjectOfType<Weapon>().weaponEquipped = this;
        FindObjectOfType<WeaponUI>().weapon = this;
        FindObjectOfType<WeaponUI>().animator.SetBool("gunIsEquipped", true);
        FindObjectOfType<WeaponUI>().updateUI();
    }
}
