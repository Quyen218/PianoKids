using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SongController : MonoBehaviour
{
    [SerializeField] GameObject[] m_noteGuides; //8 notes show what note should be press
    [SerializeField] SoundNote[] m_noteButtons;
    [SerializeField] GameObject m_notePointPrefab;
    [SerializeField] Button m_autoPlayButton;
    [SerializeField] Sprite m_pauseSprite;
    [SerializeField] Sprite[] m_noteSprites;
    [Header("Audio define for instrument")]
    [SerializeField] AudioClip[] m_pianoAudios;
    [SerializeField] AudioClip[] m_guitarAudios;
    [SerializeField] AudioClip[] m_fluteAudios;
    [SerializeField] AudioClip[] m_xylophoneAudios;

    [Header("Some constances define")]
    [SerializeField] float m_delayInAutoMode = 0.5f;
    ////////////////////////////////////////////////////////////////////////////////

    SongNotes m_notes;
    int m_currentNote;
    GameObject m_notePointer;
    Vector3 m_notePointerDest;
    private bool m_autoPlay;
    WaitForSeconds m_delayNoteAutoPlay;
    private void Start()
    {
        m_delayNoteAutoPlay = new WaitForSeconds(m_delayInAutoMode);
    }

    private void Update()
    {
        if (m_notePointer && m_notePointer.activeSelf)
        {
            if (m_notePointerDest != Vector3.zero)
            {
                Vector3 pos = Vector3.Lerp(m_notePointer.transform.position, m_notePointerDest, Time.deltaTime * 3);
                m_notePointer.transform.position = pos;

                if ((pos - m_notePointerDest).sqrMagnitude < 0.01f)
                {
                    m_notePointerDest = Vector3.zero; //stop moving
                }
            }
        }
    }

    public void StartGameWithSong(SongDefine song)
    {
        m_currentNote = -1;
        if (song.m_file)
        {
            m_notes = JsonUtility.FromJson<SongNotes>(song.m_file.text);
        }

        DisableAllNotes();
        if (m_autoPlayButton)
        {
            m_autoPlayButton.interactable = false;
        }

        if (!m_notePointer)
        {
            if (m_notePointPrefab)
            {
                m_notePointer = Instantiate(m_notePointPrefab, transform.position, Quaternion.identity, transform);
                m_notePointer.SetActive(false);
            }
        }
        else
        {
            m_notePointer.SetActive(false);
        }

        StartCoroutine(ShowFirstNode());
    }

    IEnumerator ShowFirstNode()
    {
        //NOTE: cheat to delay for train running
        yield return new WaitForSeconds(1.5f);

        ShowNextNode();
        if (m_autoPlayButton)
        {
            m_autoPlayButton.interactable = true;
        }
    }

    IEnumerator RestartSong()
    {
        yield return new WaitForSeconds(1.5f);
        SongPlayMenu.GetMenuInstance().ShowExcellent(false);

        StartCoroutine(ShowFirstNode());
    }

    void DisableAllNotes()
    {
        for (int i = 0; i < m_noteButtons.Length; i++)
        {
            m_noteButtons[i].EnableNote = false;
        }
    }
    void EnableNote(string note, bool isEnable)
    {
        ENoteDef noteDef = NoteDefine.ConvertNote(note);
        int idx = (int)noteDef;

        EnableNote(idx, isEnable);
    }

    void EnableNote(int index, bool isEnable)
    {
        if (index >= 0 && index < m_noteButtons.Length)
        {
            m_noteButtons[index].EnableNote = isEnable;
        }
    }
    void ShowNextNode()
    {
        m_currentNote++;

        if (m_currentNote < m_notes.Notes.Count)
        {
            ENoteDef noteDef = NoteDefine.ConvertNote(m_notes.Notes[m_currentNote]);
            if (noteDef == ENoteDef.Break)
            {
                ShowNextNode(); //recursive call to get real note
            }
            else
            {
                int idx = (int)noteDef;

                var anim = m_noteGuides[idx].GetComponent<Animator>();
                if (anim)
                {
                    anim.Play("throw");
                }

                EnableNote(idx, true);

                if (m_notePointer)
                {
                    m_notePointer.SetActive(true);
                    m_notePointer.transform.position = m_noteGuides[idx].transform.position;
                    int rnd = Random.Range(0, m_noteSprites.Length);
                    m_notePointer.GetComponent<Image>().overrideSprite = m_noteSprites[rnd];

                    m_notePointerDest = m_noteButtons[idx].transform.position;

                    m_noteButtons[idx].GetComponent<UIFlashAnim>().PlayAnim();
                }
            }
        }
    }
    ////////////////////////////////////////////////////////////////////////////////
    public void OnCurrentNotePlay()
    {
        if (m_currentNote == m_notes.Notes.Count - 1) //last note
        {
            EnableNote(m_notes.Notes[m_currentNote], false);
            if (m_autoPlay)
            {
                m_autoPlay = false; //turn of auto play mode when done
                m_currentNote = -1;
                if (m_autoPlayButton)
                {
                    Image img = m_autoPlayButton.GetComponent<Image>();
                    img.overrideSprite = null;
                }
                ShowNextNode();
            }
            else
            {
                PlayAnimCurrentNotePlay();
                m_currentNote = -1;
                SongPlayMenu.GetMenuInstance().ShowExcellent(true);
                m_notePointer.SetActive(false);
                StartCoroutine(RestartSong());
            }
        }
        else
        {
            if (m_autoPlay)
            {
                //
            }
            else
            {
                PlayAnimCurrentNotePlay();

                EnableNote(m_notes.Notes[m_currentNote], false);

                ShowNextNode();
            }
        }
    }

    public void OnAutoPlayBtnClick()
    {
        m_autoPlay = !m_autoPlay;
        if (m_autoPlayButton)
        {
            Image img = m_autoPlayButton.GetComponent<Image>();
            img.overrideSprite = m_autoPlay ? m_pauseSprite : null;
        }
        if (m_currentNote > 0)
        {
            EnableNote(m_notes.Notes[m_currentNote], false);
        }

        m_currentNote = -1; // reset when change auto play mode

        if (m_autoPlay)
        {
            if (m_notePointer)
            {
                m_notePointer.SetActive(false);
            }
            StartCoroutine(AutoPlay());
        }
        else
        {
            StopCoroutine(AutoPlay());
            ShowNextNode();
        }
    }

    private IEnumerator AutoPlay()
    {
        m_currentNote = 0;
        while (m_currentNote < m_notes.Notes.Count && m_autoPlay)
        {
            ENoteDef noteDef = NoteDefine.ConvertNote(m_notes.Notes[m_currentNote]);
            if (noteDef == ENoteDef.Break)
            {
                m_currentNote++;
                yield return m_delayNoteAutoPlay;
            }
            else
            {
                int idx = (int)noteDef;

                EnableNote(idx, true);

                m_noteButtons[idx].PlayNote();
                m_noteButtons[idx].PlayAnimation("buttonAuto");
                m_currentNote++;

                EnableNote(idx, false);
                yield return m_delayNoteAutoPlay;
            }
        }

    }

    private void PlayAnimCurrentNotePlay()
    {
        ENoteDef noteDef = NoteDefine.ConvertNote(m_notes.Notes[m_currentNote]);
        int idx = (int)noteDef;
        m_noteButtons[idx].PlayAnimation("buttonPress");
    }

    ////////////////////////////////////////////////////////////////////////////////
    #region Change instrument
    public void OnInstrumentChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            string instrument = toggle.gameObject.name.ToLower();
            switch (instrument)
            {
                case "piano":
                    {
                        for (int i = 0; i < m_noteButtons.Length; i++)
                        {
                            if (i < m_pianoAudios.Length)
                            {
                                m_noteButtons[i].ChangeAudio(m_pianoAudios[i]);
                            }
                        }
                    }
                    break;
                case "guitar":
                    {
                        for (int i = 0; i < m_noteButtons.Length; i++)
                        {
                            if (i < m_guitarAudios.Length)
                            {
                                m_noteButtons[i].ChangeAudio(m_guitarAudios[i]);
                            }
                        }
                    }
                    break;
                case "flute":
                    {
                        for (int i = 0; i < m_noteButtons.Length; i++)
                        {
                            if (i < m_fluteAudios.Length)
                            {
                                m_noteButtons[i].ChangeAudio(m_fluteAudios[i]);
                            }
                        }
                    }
                    break;
                case "xylophone":
                    {
                        for (int i = 0; i < m_noteButtons.Length; i++)
                        {
                            if (i < m_xylophoneAudios.Length)
                            {
                                m_noteButtons[i].ChangeAudio(m_xylophoneAudios[i]);
                            }
                        }
                    }
                    break;
            }
        }
    }
    #endregion
}