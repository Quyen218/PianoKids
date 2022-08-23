using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    const string k_sounds_folder_music = "Sounds/Music";
    const string k_sounds_folder_sfx = "Sounds/SFX";
    const string k_sounds_folder_piano = "Sounds/Piano";

    private AudioClip[] m_musicArray;
    private AudioClip[] m_SFXArray;
    private AudioClip[] m_pianoSoundsArray;

    private Dictionary<string, AudioClip> m_listMusic;
    private Dictionary<string, AudioClip> m_listSFX;
    private Dictionary<string, AudioClip> m_listPianoSounds;

    private AudioSource m_musicSource;


    public SoundNote MusicRitmosOfGuitar { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        m_musicArray = Resources.LoadAll<AudioClip>(k_sounds_folder_music);
        m_listMusic = new Dictionary<string, AudioClip>();
        for(int i=0; i<m_musicArray.Length; i++)
        {
            m_listMusic.Add(m_musicArray[i].name, m_musicArray[i]);
        }

        m_SFXArray = Resources.LoadAll<AudioClip>(k_sounds_folder_sfx);
        m_listSFX = new Dictionary<string, AudioClip>();
        for (int j = 0; j < m_SFXArray.Length; j++)
        {
            m_listSFX.Add(m_SFXArray[j].name, m_SFXArray[j]);
        }

        m_pianoSoundsArray = Resources.LoadAll<AudioClip>(k_sounds_folder_piano);
        m_listPianoSounds = new Dictionary<string, AudioClip>();
        for (int j = 0; j < m_SFXArray.Length; j++)
        {
            m_listPianoSounds.Add(m_pianoSoundsArray[j].name, m_pianoSoundsArray[j]);
        }

        if (!m_musicSource)
        {
            m_musicSource = gameObject.AddComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

   

    public void PlayMusic(string name, bool isloop = true)
    {
        if(!PianoSave.Instance.m_MusicEnable)
        {
            return;
        }

        if(Instance)
        {
            AudioClip clip = m_listMusic[name];
            if (!clip) { return; }
            if (m_musicSource.isPlaying) { return; }

            m_musicSource.clip = clip;
            m_musicSource.volume = 1f;
            m_musicSource.loop = isloop;

            m_musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if(!PianoSave.Instance.m_SFXEnable)
        {
            return;
        }

        if (Instance)
        {
            GameObject audioSFX = new GameObject(name);
            AudioSource sfxSource = audioSFX.AddComponent<AudioSource>();
            AudioClip clip = m_listSFX[name];

            sfxSource.clip = clip;
            sfxSource.volume = 1f;

            sfxSource.Play();

            Destroy(audioSFX, clip.length);
        }
    }

    public void PlaySound(string name)
    {
        Dictionary<string, AudioClip> m_listSounds = m_listPianoSounds;

        if(Instance)
        {
            GameObject audioSounds = new GameObject(name);
            AudioSource soundsSource = audioSounds.AddComponent<AudioSource>();
            AudioClip clip = m_listSounds[name];

            soundsSource.clip = clip;
            soundsSource.volume = 1f;

            soundsSource.Play();

            Destroy(audioSounds, clip.length);
        }
    }

    public void StopMusic()
    {
        if(m_musicSource)
        {
            m_musicSource.Stop();
        }
    }

    //public void ChangeVolume()
    //{
    //    if(m_musicSource)
    //    {
    //        m_musicSource.volume = SoundsVolume;
    //    }
    //}


}
