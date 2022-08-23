using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PianoSave : Singleton<PianoSave>
{

    public LANGUAGE m_lanuage { set; get; }
    public bool m_SFXEnable { set; get; }
    public bool m_MusicEnable { set; get; }
    public NOTE_STYPE m_NoteStype { set; get; }
    
    public LANGUAGE m_SoundLanguage { set; get; }

    private void Start()
    {
        UpdateAll();
    }

    public void SaveAll()
    {
        SaveSystem.SavePlayer(this);
    }

    public void UpdateAll()
    {
        SaveConfig player = SaveSystem.LoadPlayer();

        if (player != null)
        {
            m_lanuage = player.m_lanuage;
            m_SFXEnable = player.m_SFXEnable;
            m_MusicEnable = player.m_MusicEnable;
            m_NoteStype = player.m_NoteStype;
            m_SoundLanguage = player.m_SoundLanguage;

            GUIManager.Instance.UpdateLanguage();
        }
        else
        {
            m_lanuage = LANGUAGE.ENGLISH;
            m_SFXEnable = true;
            m_MusicEnable = true;
            m_NoteStype = NOTE_STYPE.DOREMI;
            m_SoundLanguage = LANGUAGE.ENGLISH;
            GUIManager.Instance.UpdateLanguage();
        }
    }

}
