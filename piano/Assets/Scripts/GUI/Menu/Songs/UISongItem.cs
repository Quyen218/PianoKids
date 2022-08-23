using UnityEngine;
using UnityEngine.UI;

public class UISongItem : MonoBehaviour
{
    [SerializeField] Text m_itemInfoText;

    private SongDefine m_songDef;
    public void Setup(SongDefine song, int index)
    {
        m_songDef = song;
        if (m_itemInfoText)
        {
            m_itemInfoText.text = (index + 1) + ". " + song.m_title;
        }
    }

    public void OnItemClick()
    {
        if (m_songDef != null)
        {
            Debug.Log("Open song: " + m_songDef.m_title);
            SoundManager.Instance.StopMusic();
            SongPlayMenu.Open(m_songDef);
        }
    }
}