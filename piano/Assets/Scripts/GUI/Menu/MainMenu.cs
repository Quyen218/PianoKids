using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MenuBase<MainMenu>
{
    // Start is called before the first frame update
    void Start()
    {

        GUIManager.Instance.AddScreentoStack(this);
        SoundManager.Instance.PlayMusic(SoundDefine.k_BackgroundName);
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBack()
    {
       /// no doing
    }

    protected override void OnMenuOpening()
    {
        base.OnMenuOpening();
        SoundManager.Instance.PlayMusic(SoundDefine.k_BackgroundName);
        
    }

    protected override void OnMenuClosing()
    {
        base.OnMenuClosing();
    }

    public void OnSettingButtonClick()
    {
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        SettingMenu.Open();
    }

    public void OnPlayBtnClick()
    {
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        ActionMenu.Open();
    }

    public void OnInstrumentsButtonClick()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        InstrumentPiano.Open();
    }

    public void OnSongsBtnClick()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        SongListMenu.Open();
    }

    public void OnSoundBtnClick ()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        SoundListMenu.Open();
    }

    public void OnShopBtnClick()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        GUIManager.Instance.m_action = PASSCODE_NEXT_ACTION.GOTOSHOP;
        PasscodeMenu.Open();
    }

    public void OnShareButtonCLick()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        GUIManager.Instance.m_action = PASSCODE_NEXT_ACTION.SHARE;
        PasscodeMenu.Open();
    }

}
