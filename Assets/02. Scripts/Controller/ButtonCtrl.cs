using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    [Header("버튼의 부모 트랜스폼")]
    [SerializeField] private Transform m_button_root;
    private Button[] m_buttons;

    private int m_current_index = 0;
    public int Index
    {
        get { return m_current_index; }
        private set { m_current_index = value; }
    }

    private void Awake()
    {
        m_buttons = m_button_root.GetComponentsInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(m_buttons[0].gameObject);   
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.PlayEffect("Button Select");
            Index = (Index - 1 + m_buttons.Length) % m_buttons.Length;
            EventSystem.current.SetSelectedGameObject(m_buttons[Index].gameObject);            
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.Instance.PlayEffect("Button Select");
            Index = (Index + 1 + m_buttons.Length) % m_buttons.Length;
            EventSystem.current.SetSelectedGameObject(m_buttons[Index].gameObject);
        }
    }

    public void Initialization()
    {
        EventSystem.current.SetSelectedGameObject(m_buttons[Index].gameObject);
    }
}
