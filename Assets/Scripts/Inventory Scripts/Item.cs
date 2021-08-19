using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Key,
    Consumable,
    Null
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    public ItemType type;
    new public string name = "New Item";
    public Sprite icon = null;
    public int ID;

    [TextArea (15,20)]
    public string description = "";


    public virtual void Use()
    {
        //Codigo sobrescribible para cada tipo de Item
    }

}
