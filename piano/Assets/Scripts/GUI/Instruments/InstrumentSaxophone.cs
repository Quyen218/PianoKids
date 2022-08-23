using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentSaxophone : MenuBase<InstrumentSaxophone>
{
    [SerializeField] List<AudioClip> m_animalSounds;
    [SerializeField] List<Sprite> m_animalImage;
    [SerializeField] Image m_animalImageShow;
    [SerializeField] Animator m_animatorAnimal;
    [SerializeField] AudioSource m_audioSourceAnimal;
    [SerializeField] Button m_buttonAnimal;
    private int m_animalCount;
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
        m_animalCount = 0;
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

    public void OnAnimalButtonClick()
    {
        m_animalImageShow.GetComponent<Image>().sprite = m_animalImage[m_animalCount];
        m_animatorAnimal.Play("ShowAnimal", -1, 0f);
        m_audioSourceAnimal.clip = m_animalSounds[m_animalCount];
        m_audioSourceAnimal.Play();

        m_buttonAnimal.interactable = false;
        StartCoroutine(EnableAnimalButton());
        m_animalCount++;
        if(m_animalCount >= m_animalImage.Count)
        {
            m_animalCount = 0;
        }
    }

    IEnumerator EnableAnimalButton()
    {
        yield return new WaitForSeconds(2);
        m_buttonAnimal.interactable = true;
    }
}
