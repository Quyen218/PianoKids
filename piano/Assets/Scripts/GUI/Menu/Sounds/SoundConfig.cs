using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "piano/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public ButtonSoundDefine[] ButtonList;
}

[System.Serializable]
public class ButtonSoundDefine
{
    public string m_title;
    public Sprite m_sprite;
    public string m_menuName;
}
