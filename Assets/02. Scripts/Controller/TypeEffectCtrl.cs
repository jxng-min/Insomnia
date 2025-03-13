using TMPro;
using UnityEngine;

public class TypeEffectCtrl : MonoBehaviour
{
    [Header("초당 작성될 문자의 비율")]
    [SerializeField] private int m_cps;

    private string m_target_text;
    private TMP_Text m_current_text;
    private int m_current_idx;
    private float m_interval;

    private void Awake()
    {
        m_current_text = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        m_target_text = text;

        EffectStart();
    }

    private void EffectStart()
    {
        m_current_text.text = "";
        m_current_idx = 0;
        DialogueManager.Instance.Cursor.SetActive(false);

        m_interval = 1.0f / m_cps;
        Invoke("Effecting", m_interval);
    }

    private void Effecting()
    {
        if(m_current_text.text == m_target_text)
        {
            EffectEnd();
            return;
        }

        m_current_text.text += m_target_text[m_current_idx++];

        Invoke("Effecting", m_interval);
    }

    private void EffectEnd()
    {
        DialogueManager.Instance.Cursor.SetActive(true);
    }
}
