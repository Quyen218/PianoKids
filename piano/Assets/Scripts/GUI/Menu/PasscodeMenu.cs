using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.UI;

public enum PASSCODE_NEXT_ACTION
{
    NONE,
    GOTOSHOP,
    SHARE
}

public class PasscodeMenu : MenuBase<PasscodeMenu>
{
    [SerializeField] private int m_limitWrongTime = 1;
    [SerializeField] Text m_numberText;
    private List<int> m_codes = new List<int>();
    private int m_currentNo = 0;
    private int m_wrongCount = 0;
    private Image[] m_correctImages;

    private const int k_code_length = 3;

    private void Start()
    {
        m_correctImages = new Image[k_code_length];
    }
    protected override void OnMenuOpening()
    {
        base.OnMenuOpening();
        GeneratePasscode();
        m_currentNo = 0;
        m_wrongCount = 0;
    }

    protected override void OnMenuClosing()
    {
        base.OnMenuClosing();
        for (int i = 0; i < m_correctImages.Length; i++)
        {
            if (m_correctImages[i])
            {
                m_correctImages[i].color = Color.white;
            }
        }
        GUIManager.Instance.m_action = PASSCODE_NEXT_ACTION.NONE;
        //gameObject.SetActive(false);
    }

    ////////////////////////////////////////////////////////////////////////////////
    public void OnNumberButtonClick(Transform button)
    {
        int number = button.GetSiblingIndex() + 1;
        if (number > 0)
        {
            if (number == m_codes[m_currentNo])
            {
                m_currentNo++;

                //change UI
                Image btnImg = button.GetComponent<Image>();
                if (btnImg)
                {
                    button.GetComponent<Image>().color = Color.gray;
                    m_correctImages[m_currentNo - 1] = btnImg;
                }

                if (m_currentNo == m_codes.Count)
                {
                    OnCorrectPassCode();
                }
               
            }
            else
            {
                OnWrongPasscode();
            }
        }
        else
        {
            OnWrongPasscode();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////
    private void GeneratePasscode()
    {
        m_codes.Clear();
        for (int i = 0; i < k_code_length; i++)
        {
            int rand = Random.Range(0, 9) + 1;
            while (m_codes.Contains(rand))
            {
                rand = Random.Range(0, 9) + 1;
            }

            m_codes.Add(rand);
        }

        string str = ShowCodeString();
        m_numberText.text = str;
    }

    private string ShowCodeString()
    {
        string str = string.Empty;
        for (int i = 0; i < m_codes.Count; i++)
        {
            str += GetNumberString(m_codes[i]);
            if (i < m_codes.Count - 1)
            {
                str += ", ";
            }
        }
        return str;
    }
    private void OnWrongPasscode()
    {
        m_wrongCount++;
        if (m_wrongCount >= m_limitWrongTime)
        {
            OnBack();
        }
        else
        {
            m_currentNo = 0; //reset
            for (int i = 0; i < m_correctImages.Length; i++)
            {
                if (m_correctImages[i])
                {
                    m_correctImages[i].color = Color.white;
                }
            }
        }
    }

    private void OnCorrectPassCode()
    {
        switch(GUIManager.Instance.m_action)
        {
            case PASSCODE_NEXT_ACTION.GOTOSHOP:
                ShopMenu.Open();
                break;
            case PASSCODE_NEXT_ACTION.SHARE:
                StartCoroutine(CreateImageAndShare());
                GUIManager.Instance.GotoHome();
                break;
        }
        Debug.Log("correct code ");
    }

    private string GetNumberString(int number)
    {
        LANGUAGE current = PianoSave.Instance.m_lanuage;
        string key = "Num." + number;
        return GetLocalizeString(key);
    }

    string GetLocalizeString(string key)
    {
        return LocalizationManager.Localize(key);
    }

    private IEnumerator CreateImageAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = Resources.Load<Texture2D>("Share/Piano_Kids_ENG");
        Texture2D output = new Texture2D(texture.width, texture.height);
        output.SetPixels(texture.GetPixels());
        output.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, output.EncodeToPNG());

        // To avoid memory leaks
        Destroy(output);

        new NativeShare().AddFile(filePath).SetSubject("").SetText("").Share();

    }
}
