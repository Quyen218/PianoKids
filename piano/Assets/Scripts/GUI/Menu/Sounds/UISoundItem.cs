using UnityEngine;
using UnityEngine.UI;

public class UISoundItem : MonoBehaviour
{
    [SerializeField] Image m_itemInfoImage;

    private ButtonSoundDefine m_ButtonSoundDef;
    public void Setup(ButtonSoundDefine buttonSound)
    {
        m_ButtonSoundDef = buttonSound;
        if (m_itemInfoImage)
        {
            m_itemInfoImage.sprite = buttonSound.m_sprite;
        }
    }

    public void OnItemClick()
    {
        if (m_ButtonSoundDef != null)
        {
            Debug.Log("Open Menu: " + m_ButtonSoundDef.m_title);
            GUIManager.Instance.OpenMenu(m_ButtonSoundDef.m_menuName);
            SoundManager.Instance.StopMusic();
            //SongPlayMenu.Open(m_songDef);
        }
    }
}