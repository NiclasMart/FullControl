using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
   
    public static void SaveData(int level) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.fc";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, level);
        stream.Close();
    }

    public static int LoadData() {
        string path = Application.persistentDataPath + "/level.fc";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int level = (int)formatter.Deserialize(stream);
            stream.Close();

            return level;
            
        }
        else {
            Debug.LogWarning("Save file not found in " + path);
            return -1;
        }
    }

    public static bool SaveDataExists() {
        string path = Application.persistentDataPath + "/level.fc";
        return File.Exists(path);
    }

    public static void DeleteSaveGame() {
        string path = Application.persistentDataPath + "/level.fc";
        if (File.Exists(path)) {
            File.Delete(path);
        }
    }

}
