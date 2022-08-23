using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentFlute : MenuBase<InstrumentFlute>
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
        AdsManager.Instance.ShowBanner();
        AdsManager.Instance.ShowInterstitial();
    }

    protected override void OnMenuClosing()
    {
        base.OnMenuClosing();
        AdsManager.Instance.HideBanner();
    }

    public override void OnBack()
    {
        OnHomeButtonCLick();
        ////nothing
    }

    public void OnHomeButtonCLick()
    {
        //GUIManager.Instance.GotoHome();
        ActionMenu.Open();
    }

    public void OnPianoButtonClick()
    {
        InstrumentPiano.Open();
    }

    public void OnXylophoneButtonClick()
    {
        InstrumentXylophone.Open();
    }

    public void OnDrumsButtonClick()
    {
        InstrumentDrums.Open();
    }

    public void OnFluteButtonClick()
    {
        InstrumentFlute.Open();
    }

    public void OnGuitarButtonClick()
    {
        InstrumentGuitar.Open();
    }

    public void OnSaxophoneButtonClick()
    {
        InstrumentSaxophone.Open();
    }
}
