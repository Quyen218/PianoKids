using UnityEngine;

public class UIFlashAnim : MonoBehaviour {
    [SerializeField] Animator m_animator;

    public void PlayAnim()
    {
        if (m_animator)
        {
            m_animator.Play("buttonFlash");
        }
    }
}