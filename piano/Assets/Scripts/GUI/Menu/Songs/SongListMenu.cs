using System.Collections.Generic;
using UnityEngine;

public class SongListMenu : MenuBase<SongListMenu>
{
    [SerializeField] SongConfig m_config;
    [SerializeField] UISongPage m_pagePrefab;
    [SerializeField] UISongItem m_itemPrefab;
    [SerializeField] Transform m_scrollContentParent;
    [SerializeField] ScrollSnapRect m_scrollRect;
    [SerializeField] GameObject m_pageIndicateTmpl; //also use it as first object
    private List<UISongPage> m_pages = new List<UISongPage>();

    ////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        // Setup();
    }
    protected override void OnMenuClosing()
    {
        // Cleanup();
        base.OnMenuClosing();
    }

    protected override void OnMenuOpening()
    {
        base.OnMenuOpening();
        SetupOnce();
        SoundManager.Instance.PlayMusic(SoundDefine.k_BackgroundName);
    }
    void SetupOnce()
    {
        if (m_config != null) 
        {
            // already setup 
            if (m_pages.Count != 0) return;

            SongDefine[] itemArray = m_config.SongList;
            int count = 0;
            int pageCount = 0;
            while (count < itemArray.Length)
            {
                UISongPage page;
                if (pageCount < m_pages.Count && m_pages[pageCount])
                {
                    page = m_pages[pageCount];
                }
                else
                {
                    page = Instantiate<UISongPage>(m_pagePrefab, m_scrollContentParent, false);
                    m_pages.Add(page);
                }
                ++pageCount;
                page.Setup(m_config, count, m_itemPrefab);
                int itemAdded = page.GetItemInited();
                count += itemAdded;
            }

            if (m_pageIndicateTmpl)
            {
                for (int i = 0; i < pageCount - 1; i++)
                {
                    GameObject obj = Instantiate(m_pageIndicateTmpl,
                        m_pageIndicateTmpl.transform.position,
                        Quaternion.identity,
                        m_pageIndicateTmpl.transform.parent);
                }
            }
        }
        else
        {
            //shop not enable
            Debug.Log("Shop Is not Available");
            m_scrollRect.enabled = false;
        }
    }

    void Cleanup()
    {
        foreach (var page in m_pages)
        {
            page.CleanUp();
        }
    }

    public void OnPageSelectChanged()
    {
        int page = m_scrollRect.CurrentPage;
        if (m_scrollRect.prevButton)
            m_scrollRect.prevButton.SetActive(page != 0);
        if (m_scrollRect.nextButton)
            m_scrollRect.nextButton.SetActive(page != m_pages.Count - 1);
    }
}