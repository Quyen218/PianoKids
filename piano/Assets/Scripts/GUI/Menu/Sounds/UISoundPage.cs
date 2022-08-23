using UnityEngine;

public class UISoundPage : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int m_maxItem = 6;
    private int m_itemCount;

    public void Setup(SoundConfig config, int pItemStart, UISoundItem itemPrefab)
    {
        for (int i = 0; i < m_maxItem; i++)
        {
            if (m_itemCount > m_maxItem || m_itemCount + pItemStart >= config.ButtonList.Length)
            {
                break;
            }

            ++m_itemCount;
            // setup item from item start
            var item = Instantiate<UISoundItem>(itemPrefab, itemPrefab.transform.position, Quaternion.identity, transform);
            item.Setup(config.ButtonList[pItemStart + i]);
        }
    }
    public int GetItemInited()
    {
        return m_itemCount;
    }

    public void CleanUp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        m_itemCount = 0;
    }

}