using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffectCtrl : MonoBehaviour
{
    private GameObject m_end_cursor;

    public int m_cps;
    private string m_target_text;
    private TMP_Text m_current_text;
    private int m_current_idx;
    private float m_interval;

    private void Awake()
    {
        m_current_text = GetComponent<TMP_Text>();
        m_end_cursor = ObjectFindManager.FindInactiveObject("Cursor_Dialogue");
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
        m_end_cursor.SetActive(false);

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
        m_end_cursor.SetActive(true);
    }

    public bool GetCursorState()
    {
        if(GameObject.Find("Cursor_Dialogue") == null)
            return false;
        else
            return true;
    }
}
