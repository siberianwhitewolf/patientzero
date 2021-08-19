using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Inventory/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{

    public Item[] Items;
    public Dictionary<Item, int> GetID = new Dictionary<Item, int>();
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();

    public void OnAfterDeserialize()
    {
            GetID = new Dictionary<Item, int>();
            for (int i = 0; i < Items.Length; i++)
            {
            // Si existe el item en esa posicion, lo agrego al diccionario.
            if (Items[i] != null)
            {
               GetID.Add(Items[i], i);
               GetItem.Add(i, Items[i]);
            }
            }
    }

    public void OnBeforeSerialize()
    {

    }

}
