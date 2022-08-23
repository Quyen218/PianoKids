using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LANGUAGE
{
    BRAZIL,
    ENGLISH,
    ESPANOL
}

public enum NOTE_STYPE
{
    DOREMI,
    ABC
}

[System.Serializable]
public class SaveConfig
{
    public LANGUAGE m_lanuage;
    public bool m_SFXEnable;
    public bool m_MusicEnable;
    public NOTE_STYPE m_NoteStype;

    public LANGUAGE m_SoundLanguage;

    public SaveConfig(PianoSave save)
    {
        m_lanuage = save.m_lanuage;
        m_SFXEnable = save.m_SFXEnable;
        m_MusicEnable = save.m_MusicEnable;
        m_NoteStype = save.m_NoteStype;
        m_SoundLanguage = save.m_SoundLanguage;
    }
}
