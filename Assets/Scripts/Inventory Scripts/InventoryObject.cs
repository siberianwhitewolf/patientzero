using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Object")]

public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{

    public string savePath;
    public List<Slot> Container = new List<Slot>();
    public int InventorySize = 8;
    public ItemDatabase database;


    public void CreateInventory()
    {
        for (int i = 0; i < InventorySize; i++)
        {
            Container.Add(new Slot(new NullItem(), 0, 0));
        }

    }


    public void AddItem(Item item, int amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item && Container[i].item.type != ItemType.Null)
            {
                if (Container[i].item == item)
                {
                    Container[i].AddAmount(amount);

                    if(Container[i].item.ID == 7)
                    {
                        ConsumableItem bulletBox = (ConsumableItem)Container[i].item;
                        bulletBox.restoreHealthValue += bulletBox.maxRestoreHealthValue;
                    }

                    return;

                }
            }


            else
            {
                Container[i].AddItem(database.GetID[item],item, amount, database);
                if(Container[i].item.ID == 7)
                {
                    ConsumableItem bulletBox = (ConsumableItem)Container[i].item;
                    if(bulletBox.restoreHealthValue < bulletBox.maxRestoreHealthValue)
                    {
                        bulletBox.restoreHealthValue = bulletBox.maxRestoreHealthValue;
                    }
                }

                return;

            }
        }
    }

    public void RemoveItem(Slot slot)
    {
        ClearSlot(slot);
    }

    private void ClearSlot(Slot slot)
    {
        slot.item = null;
        slot.amount = 0;
    }

    public void OnBeforeSerialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }


    public void OnAfterDeserialize()
    {

    }


    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }

    }
}

    [System.Serializable]
    public class Slot
    {

    public int ID;
    public Item item;
    public int amount;
    public ItemDatabase database;

    public Slot(Item item, int amount, int ID)
        {
        this.ID = ID;
        this.item = item;
        this.amount = amount;
        }

    public void AddItem(int id, Item item, int amount, ItemDatabase database)
    {
        this.ID = id;
        this.amount = amount;
        this.item = item;
        this.database = database;
        
    }

    public void AddAmount(int value)
        {
            amount += value;
        }

    public void SubstractAmount(int value)
    {
        amount -= value;
    }

    public void UpdateItemID(int ID)
    {
        this.ID = ID;
        this.item = database.GetItem[ID];
    }

}