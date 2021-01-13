using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem  // système de sauvegarde du jeux
{


    public static void SavePlayer(Unit player, string scene) // sauvegarde les données du joueur ( emplacement, vie, or , expérience )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerStat.xml";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, scene);



        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPlayer() // recharge les données du joueur
    {
        string path = Application.persistentDataPath + "/playerStat.xml";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerStat = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerStat;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveEquipment() // sauvegarde l'équipement du joueur
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerEq.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        List<int> PlayerEq = new List<int>();
        foreach (var item in EquipmentManager.instance.slots)
        {
            if (item.item != null)
            {
                PlayerEq.Add(item.item.itemId);
            }

        }
        formatter.Serialize(stream, PlayerEq);
        stream.Close();
    }

    public static List<int> LoadEquipment() // recharge l'équipement du joueur
    {
        string path = Application.persistentDataPath + "/playerEq.xml";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<int> PlayerEqDl = formatter.Deserialize(stream) as List<int>;
            stream.Close();
            return PlayerEqDl;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
    public static void SaveInventory() // sauvegarde l'inventaire du joueur
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        List<int> PlayerItem= new List<int>();
        foreach (var item in Inventory.instance.items)
        {
            PlayerItem.Add(item.itemId);

        }
        formatter.Serialize(stream, PlayerItem);
        stream.Close();
    }
    public static void SaveQuest(List<Quest> q)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerQuest.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        QuestData PlayerQuest = new QuestData(q);
        formatter.Serialize(stream, PlayerQuest);
        stream.Close();
    }
    public static QuestData LoadQuest() // recharge les quêtes du joueur
    {
        string path = Application.persistentDataPath + "/playerQuest.xml";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            QuestData playerQuest = formatter.Deserialize(stream) as QuestData;
            stream.Close();
            return playerQuest;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }

    }

    public static List<int> LoadInventory() // recharge l'inventaire du joueur 
    {
        string path = Application.persistentDataPath + "/player.xml";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<int> PlayerItemDl = formatter.Deserialize(stream) as List<int>;
            stream.Close();
            return PlayerItemDl;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
        
    }
   
}
