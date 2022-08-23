using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorUtil
{
    [MenuItem("GameTools/Txt to Json")]
    public static void ConverTxtToJson()
    {
        string dirPath = "Assets/SongJson";

        DirectoryInfo d = new DirectoryInfo(dirPath);
        FileInfo[] Files = d.GetFiles("*.txt", SearchOption.AllDirectories); //Getting files

        Debug.Log("found txt files: " + Files.Length);
        foreach (var file in Files)
        {
            string content = File.ReadAllText(file.FullName);
            string[] array = content.Split(' ');

            // SongNotes song = new SongNotes();
            // song.Notes = new System.Collections.Generic.List<string>();
            // song.Notes.AddRange(array);

            // string jsonStr = JsonUtility.ToJson(song);
            // string fileName = file.Name.Substring(0, file.Name.IndexOf(file.Extension));
            // string jsonPath = Path.Combine(dirPath, fileName + ".json");
            // File.WriteAllText(jsonPath, jsonStr);
        }
        Debug.Log("Convert done!");
    }

    [MenuItem("GameTools/Clear PlayerPref")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}