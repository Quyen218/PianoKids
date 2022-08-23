using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MenuBase<SettingMenu>
{
    [SerializeField] Button m_language;
    [SerializeField] Button m_sounds;
    [SerializeField] Button m_bgMusic;
    [SerializeField] Button m_scale;

    [SerializeField] List<Sprite> m_listLanguageSprite;
    [SerializeField] Sprite m_onSprite;
    [SerializeField] Sprite m_offSprite;
    [SerializeField] Sprite m_abcSprite;
    [SerializeField] Sprite m_doremiSprite;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLanguageButton();
        UpdateSFXButton();
        UpdateBackgrounMusicButton();
        UpdateScaleButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLanguageButtonCLick()
    {
        LANGUAGE current = PianoSave.Instance.m_lanuage;

        switch(current)
        {
            case LANGUAGE.BRAZIL:
                PianoSave.Instance.m_lanuage = LANGUAGE.ENGLISH;
                LocalizationManager.Language = "English";
                break;
            case LANGUAGE.ENGLISH:
                PianoSave.Instance.m_lanuage = LANGUAGE.ESPANOL;
                LocalizationManager.Language = "Espanol";
                break;
            case LANGUAGE.ESPANOL:
                PianoSave.Instance.m_lanuage = LANGUAGE.BRAZIL;
                LocalizationManager.Language = "Brazil";
                break;
        }
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        UpdateLanguageButton();
        PianoSave.Instance.SaveAll();
    }

    public void OnSoundsButtonClick()
    {
        PianoSave.Instance.m_SFXEnable = !PianoSave.Instance.m_SFXEnable;
        UpdateSFXButton();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        PianoSave.Instance.SaveAll();
    }

    public void OnBackgroundMusicClick()
    {
        PianoSave.Instance.m_MusicEnable = !PianoSave.Instance.m_MusicEnable;
        UpdateBackgrounMusicButton();
        PianoSave.Instance.SaveAll();
    }

    public void OnScaleClick()
    {
        if(PianoSave.Instance.m_NoteStype == NOTE_STYPE.ABC)
        {
            PianoSave.Instance.m_NoteStype = NOTE_STYPE.DOREMI;
        }
        else
        {
            PianoSave.Instance.m_NoteStype = NOTE_STYPE.ABC;
        }
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        UpdateScaleButton();
        PianoSave.Instance.SaveAll();
    }

    private void UpdateLanguageButton()
    {
        LANGUAGE lang = PianoSave.Instance.m_lanuage;

        switch (lang)
        {
            case LANGUAGE.BRAZIL:
                m_language.GetComponent<Image>().sprite = m_listLanguageSprite[0];
                break;
            case LANGUAGE.ENGLISH:
                m_language.GetComponent<Image>().sprite = m_listLanguageSprite[2];
                break;
            case LANGUAGE.ESPANOL:
                m_language.GetComponent<Image>().sprite = m_listLanguageSprite[1];
                break;
        }
    }

    private void UpdateSFXButton()
    {
        if (PianoSave.Instance.m_SFXEnable)
        {
            m_sounds.GetComponent<Image>().sprite = m_onSprite;
        }
        else
        {
            m_sounds.GetComponent<Image>().sprite = m_offSprite;
        }
    }

    private void UpdateBackgrounMusicButton()
    {
        if (PianoSave.Instance.m_MusicEnable)
        {
            m_bgMusic.GetComponent<Image>().sprite = m_onSprite;
            SoundManager.Instance.PlayMusic(SoundDefine.k_BackgroundName);
        }
        else
        {
            m_bgMusic.GetComponent<Image>().sprite = m_offSprite;
            SoundManager.Instance.StopMusic();
        }
    }

    private void UpdateScaleButton()
    {
        switch(PianoSave.Instance.m_NoteStype)
        {
            case NOTE_STYPE.DOREMI:
                m_scale.GetComponent<Image>().sprite = m_doremiSprite;
                break;
            case NOTE_STYPE.ABC:
                m_scale.GetComponent<Image>().sprite = m_abcSprite;
                break;
        }
    }
}
