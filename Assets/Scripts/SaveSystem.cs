using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(playerStats stats)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = string.Concat(Application.persistentDataPath, "/player.save");
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(stats);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);
           PlayerData data = formatter.Deserialize(stream) as PlayerData;
           stream.Close();
           return data;
        }

        else
        {
            Debug.Log("save file not found in "+path);
            return null;
        }
    }

    public static void SaveInteractable(InteractableObject interactable, int id)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = string.Concat(Application.persistentDataPath, $"/interactable{id}.save");
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        InteractableData data = new InteractableData(interactable);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }


    public static InteractableData LoadInteractable(int id)
    {
        string path = string.Concat(Application.persistentDataPath, $"/interactable{id}.save");
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InteractableData data = formatter.Deserialize(stream) as InteractableData;
            stream.Close();
            return data;
        }

        else
        {
            Debug.Log("save file not found in " + path);
            return null;
        }
    }


    public static void SaveEnemy(Enemy enemy, int id)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = string.Concat(Application.persistentDataPath, $"/enemy{id}.save");
        FileStream stream = new FileStream(path, FileMode.Create);
        EnemyData data = new EnemyData(enemy);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }


    public static EnemyData LoadEnemy(int id)
    {
        string path = Application.persistentDataPath + $"/enemy{id}.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
            return data;
        }

        else
        {
            Debug.Log("save file not found in " + path);
            return null;
        }
    }

}
