using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimal2 : MenuBase<SoundAnimal2>
{
    [SerializeField] List<SoundNote> m_listSoundNote;

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

}
