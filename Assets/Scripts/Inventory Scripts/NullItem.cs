using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Empty Item", menuName = "Inventory/Empty")]
public class NullItem : Item
{
    private void Awake()
    {
        type = ItemType.Null;
    }
}

