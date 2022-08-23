using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public const string saveFileName = "2048.remake.Save";

    public static void SavePlayer (PianoSave save)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + saveFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveConfig profile = new SaveConfig(save);

        formatter.Serialize(stream, profile);

        stream.Close();
    }


    public static SaveConfig LoadPlayer()
    {
        string path = Application.persistentDataPath + "/" + saveFileName;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveConfig profile = formatter.Deserialize(stream) as SaveConfig;
            stream.Close();

            return profile;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }
}
