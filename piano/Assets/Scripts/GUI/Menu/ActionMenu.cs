using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu  : MenuBase<ActionMenu>
{


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
        SoundManager.Instance.PlayMusic(SoundDefine.k_BackgroundName);
    }

    public override void OnBack()
    {
        GUIManager.Instance.GotoHome();
    }

    public void InstrumentBtnCLick()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        InstrumentPiano.Open();
    }

    public void SoundsBtnClick()
    {
        // SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        SoundListMenu.Open();
    }

    public void SongsBtnCLick()
    {
        // SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundDefine.k_ButtonClickSFXName);
        SongListMenu.Open();
    }
}
