using UnityEngine;

[CreateAssetMenu(fileName = "LocalizeString", menuName = "piano/LocalizeString")]
public class LocalizeString : ScriptableObject
{
    [SerializeField] LangStrPair[] m_strings;

    public string GetLocalizeString(string id, LANGUAGE lang)
    {
        for (int i = 0; i < m_strings.Length; i++)
        {
            if (m_strings[i].m_id.Equals(id))
            {
                return m_strings[i].GetLangString(lang);
            }
        }

        return "Not found string: " + id;
    }
}

[System.Serializable]
public class LangStr {
    public LANGUAGE m_lang;
    public string m_string;
}

[System.Serializable]
public class LangStrPair {
    public string m_id;
    public LangStr[] m_localStrings;

    public string GetLangString(LANGUAGE lang)
    {
        for (int i = 0; i < m_localStrings.Length; i++)
        {
            if (m_localStrings[i].m_lang == lang)
            {
                return m_localStrings[i].m_string;
            }
        }

        return "m_id-" + lang;
    }
}