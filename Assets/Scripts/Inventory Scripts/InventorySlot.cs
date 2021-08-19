using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Item item;
    public int ID;
    public InventoryObject inventory;
    public TextMeshProUGUI textMeshPro;

    private void Update()
    {
        if (inventory.Container.Count > ID)
        {
            if (inventory.Container[ID].item)
            {
                if (inventory.Container[ID].item is WeaponItem)
                {
                    textMeshPro.text = ((WeaponItem)inventory.Container[ID].item).currentBulletsInMagazine.ToString();
                    FindObjectOfType<WeaponUI>().GunSlot = this.ID;
                }

                else if (inventory.Container[ID].item is ConsumableItem && inventory.Container[ID].item.ID == 7)
                {
                    textMeshPro.text = ((ConsumableItem)inventory.Container[ID].item).restoreHealthValue.ToString();
                    FindObjectOfType<WeaponUI>().AmmoSlot = this.ID;
                }

                else if (inventory.Container[ID].item is ConsumableItem && !(inventory.Container[ID].item.ID == 7))
                {
                    if (inventory.Container[ID].amount > 1)
                    {
                        textMeshPro.text = inventory.Container[ID].amount.ToString();
                    }
                    else
                    {
                        textMeshPro.text = "";
                    }
                     
                }

                    AddItem(inventory.Container[ID].item);

            }

            else
            {
                ClearSlot();
            }
        }
       
    }

    public void AddItem(Item newItem)
    {
        if (newItem.type != ItemType.Null)
        {
            this.item = newItem;
            icon.sprite = newItem.icon;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {

        this.item = null;
        this.icon.sprite = null;
        this.icon.enabled = false;

    }

    public void RemoveItem(int pos)
    {
        inventory.RemoveItem(inventory.Container[pos]);
    }
}
