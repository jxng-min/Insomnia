using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtonCtrl : MonoBehaviour
{
    public Button[] m_buttons;
    private int m_current_idx = 0;

    private void Start()
    {
        if(m_buttons.Length >= 0f)
            EventSystem.current.SetSelectedGameObject(m_buttons[m_current_idx].gameObject);
    }

    private void Update()
    {
        if(GameObject.Find("Title_Inner_Panel") == null && GameObject.Find("Item_Inner_Panel") == null && GameObject.Find("Save_Inner_Panel") == null)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                SoundManager.Instance.ButtonSelect();
                m_current_idx = (m_current_idx - 1 + m_buttons.Length) % m_buttons.Length;
                EventSystem.current.SetSelectedGameObject(m_buttons[m_current_idx].gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundManager.Instance.ButtonSelect();
                m_current_idx = (m_current_idx + 1 + m_buttons.Length) % m_buttons.Length;
                EventSystem.current.SetSelectedGameObject(m_buttons[m_current_idx].gameObject);
            }
            else if(Input.GetButtonDown("Jump"))
                SoundManager.Instance.ButtonClick();
        }
    }
}
