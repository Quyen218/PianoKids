using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongConfig", menuName = "piano/SongConfig")]
public class SongConfig : ScriptableObject
{
    public SongDefine[] SongList;
}

[System.Serializable]
public class SongDefine
{
    public string m_title;
    public TextAsset m_file;
}

[System.Serializable]
public class SongNotes
{
    public List<string> Notes;
}