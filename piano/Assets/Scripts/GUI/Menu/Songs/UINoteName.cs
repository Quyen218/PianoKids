using UnityEngine;
using UnityEngine.UI;

public class UINoteName : MonoBehaviour
{
    [SerializeField] Text m_name;

    [SerializeField] ENoteDef m_note;

    private void OnEnable()
    {
        SetName();
    }

    private void SetName()
    {
        NOTE_STYPE noteType = PianoSave.Instance.m_NoteStype;
        string name = string.Empty;
        switch (m_note)
        {
            case ENoteDef.DO:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "C";
                    }
                    else
                    {
                        name = "DO";
                    }
                }
                break;
            case ENoteDef.RE:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "D";
                    }
                    else
                    {
                        name = "RE";
                    }
                }
                break;
            case ENoteDef.MI:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "E";
                    }
                    else
                    {
                        name = "MI";
                    }
                }
                break;
            case ENoteDef.FA:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "F";
                    }
                    else
                    {
                        name = "FA";
                    }
                }
                break;
            case ENoteDef.SOL:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "G";
                    }
                    else
                    {
                        name = "SOL";
                    }
                }
                break;
            case ENoteDef.LA:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "A";
                    }
                    else
                    {
                        name = "LA";
                    }
                }
                break;
            case ENoteDef.TI:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "B";
                    }
                    else
                    {
                        name = "TI";
                    }
                }
                break;
            case ENoteDef.DO2:
                {
                    if (noteType == NOTE_STYPE.ABC)
                    {
                        name = "C";
                    }
                    else
                    {
                        name = "DO";
                    }
                }
                break;
        }

        if (m_name)
        {
            m_name.text = name;
        }
    }
}
