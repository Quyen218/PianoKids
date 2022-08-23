using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TOUCH_ACTION
{
    ENTER,
    PRESS,
    CLICK
}

[SelectionBase]
public class SoundNote : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] AudioSource audio;

    [SerializeField] Image m_buttonOfInstrument;
    [SerializeField] Color[] colorsPress;
    [SerializeField] bool m_enableNote = true;
    [SerializeField] TOUCH_ACTION m_action;
    [SerializeField] bool m_isRitmos = false;
    [SerializeField] UnityEvent m_notePlayEvent;
    [SerializeField] Animator m_animator;

    public bool EnableNote
    {
        get { return m_enableNote; }
        set { m_enableNote = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_action == TOUCH_ACTION.CLICK)
        {
            PlayAnimation("NOTE_Click");
            if (m_isRitmos)
            {
                SoundNote so = SoundManager.Instance.MusicRitmosOfGuitar;

                if (so != null)
                {
                    AudioSource au = so.GetComponent<AudioSource>();
                    if (this.Equals(so))
                    {
                        so.m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[0];
                        au.Stop();
                        SoundManager.Instance.MusicRitmosOfGuitar = null;
                    }
                    else
                    {
                        so.m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[0];
                        au.Stop();
                        m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[1];
                        PlayNote();
                        SoundManager.Instance.MusicRitmosOfGuitar = this;
                    }
                }
                else
                {
                    m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[1];
                    PlayNote();
                    SoundManager.Instance.MusicRitmosOfGuitar = this;
                }
            }
            else
            {
                PlayNote();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_action == TOUCH_ACTION.PRESS)
        {
            m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[1];
            PlayNote();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!m_isRitmos)
        {
            m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[0];
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!m_isRitmos)
        {
            m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[0];
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (m_action == TOUCH_ACTION.ENTER)
        {
            m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[1];
            PlayNote();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSoundNoteToDefault()
    {
        audio.Stop();
        m_buttonOfInstrument.GetComponent<Image>().color = colorsPress[0];
    }

    public void PlayNote()
    {
        if (EnableNote)
        {
            audio.Play();
            if (m_notePlayEvent != null)
            {
                m_notePlayEvent.Invoke();
            }
        }
    }

    public void ChangeAudio(AudioClip clip)
    {
        if (audio)
        {
            audio.clip = clip;
        }
    }

    public void PlayAnimation(string animation)
    {
        if (m_animator)
        {
            m_animator.Play(animation);
        }
    }

    IEnumerator StartAnimRandomTime(string animation)
    {
        float time = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(time);

        PlayAnimation(animation);
    }

    public void PlayAnimRandomTime(string animation)
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(StartAnimRandomTime(animation));
        }
    }
}
