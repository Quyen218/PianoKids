using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGeometry : MenuBase<SoundGeometry>
{
    private LANGUAGE m_language;

    [SerializeField] Animator m_animControl;
    [SerializeField] List<AudioSource> m_audioSource;
    [SerializeField] List<SoundNote> m_listSoundNote;
    [SerializeField] List<AudioClip> soundList_BR;
    [SerializeField] List<AudioClip> soundList_EN;
    [SerializeField] List<AudioClip> soundList_ES;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnMenuOpening()
    {
        base.OnMenuOpening();
        AdsManager.Instance.ShowBanner();
        AdsManager.Instance.ShowInterstitial();
        m_language = PianoSave.Instance.m_SoundLanguage;
        ChangeLanguage(m_language);
    }

    protected override void OnMenuClosing()
    {
        base.OnMenuClosing();
        AdsManager.Instance.HideBanner();
    }


    public void StartAnimIdle()
    {
        for (int i = 0; i < m_listSoundNote.Count; i++)
        {
            m_listSoundNote[i].PlayAnimRandomTime("NOTE_Idle");
        }
    }

    public void OnHomeBtnClick()
    {
        //GUIManager.Instance.GotoHome();
        ActionMenu.Open();
    }

    public void OnBRButtonCLick()
    {
        ChangeLanguage(LANGUAGE.BRAZIL);
    }

    public void OnENButtonClick()
    {
        ChangeLanguage(LANGUAGE.ENGLISH);
    }

    public void OnESButtonClick()
    {
        ChangeLanguage(LANGUAGE.ESPANOL);
    }

    public void ChangeLanguage(LANGUAGE lang)
    {
        switch(lang)
        {
            case LANGUAGE.BRAZIL:
                m_animControl.Play("ShowPanelAnim_BR", -1, 0f);
                for (int i = 0; i< soundList_BR.Count; i++)
                {
                    m_audioSource[i].clip = soundList_BR[i];
                }
                break;
            case LANGUAGE.ENGLISH:
                m_animControl.Play("ShowPanelAnim_EN", -1, 0f);
                for (int i = 0; i < soundList_EN.Count; i++)
                {
                    m_audioSource[i].clip = soundList_EN[i];
                }
                break;
            case LANGUAGE.ESPANOL:
                m_animControl.Play("ShowPanelAnim_ES", -1, 0f);
                for (int i = 0; i < soundList_ES.Count; i++)
                {
                    m_audioSource[i].clip = soundList_ES[i];
                }
                break;
        }
        PianoSave.Instance.m_SoundLanguage = lang;
        PianoSave.Instance.SaveAll();
    }
}
