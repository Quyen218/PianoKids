using UnityEngine;
using UnityEngine.UI;

public class SongPlayMenu : MenuBase<SongPlayMenu>
{
    #region Static function
    public static void Open(SongDefine song)
    {
        s_currentSong = song;
        Open();
    }

    static SongDefine s_currentSong;
    #endregion

    #region Serialized fields
    [SerializeField] Text m_songTitle;
    [SerializeField] SongController m_songController;
    [SerializeField] GameObject m_excellentObj;
    [SerializeField] Animator m_trainAnimator;
    #endregion

    void Start()
    {

    }

    #region Menu Methods
    public void OnHomeButtonCLick()
    {
        //GUIManager.Instance.GotoHome();
        ActionMenu.Open();
    }

    ////////////////////////////////////////////////////////////////////////////////
    protected override void OnMenuOpening()
    {
        base.OnMenuOpening();
        Setup();
        AdsManager.Instance.ShowBanner();
        AdsManager.Instance.ShowInterstitial();
    }

    protected override void OnMenuClosing()
    {
        Cleanup();
        AdsManager.Instance.HideBanner();
        base.OnMenuClosing();
    }
    ////////////////////////////////////////////////////////////////////////////////

    void Setup()
    {
        if (s_currentSong != null)
        {
            if (m_songTitle)
            {
                m_songTitle.text = s_currentSong.m_title;
            }

            if (m_songController)
            {
                m_songController.StartGameWithSong(s_currentSong);
            }
        }
    }

    void Cleanup()
    {
        s_currentSong = null;
    }

    public void ShowExcellent(bool isShow)
    {
        if (m_excellentObj)
        {
            m_excellentObj.SetActive(isShow);
            AudioSource audio = m_excellentObj.GetComponent<AudioSource>();
            if (audio)
            {
                audio.Play();
            }
        }

        if (m_trainAnimator)
        {
            if (isShow)
            {
                m_trainAnimator.Play("train_disappear");
            }
            else
            {
                m_trainAnimator.Play("train_appear");
            }
        }
    }
    #endregion
}
