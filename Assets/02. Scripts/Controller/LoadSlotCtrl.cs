using UnityEngine;
using UnityEngine.EventSystems;

public class LoadSlotCtrl : MonoBehaviour
{
    private Animator m_load_ui_animator;

    [Header("슬롯들의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;
    private SaveSlot[] m_slots;
    public SaveSlot[] Slots
    {
        get { return m_slots; }
    }

    private int m_current_idx = 0;
    public int Index
    {
        get { return m_current_idx; }
        set { m_current_idx = value; }
    }

    private void Awake()
    {
        m_slots = m_slot_root.GetComponentsInChildren<SaveSlot>();

        m_load_ui_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(m_load_ui_animator.GetBool("Open") is true)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                SoundManager.Instance.PlayEffect("Button Select");
                Index = (Index - 1 + m_slots.Length) % m_slots.Length;
                EventSystem.current.SetSelectedGameObject(m_slots[Index].gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundManager.Instance.PlayEffect("Button Select");
                Index = (Index + 1 + m_slots.Length) % m_slots.Length;
                EventSystem.current.SetSelectedGameObject(m_slots[Index].gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                m_load_ui_animator.SetBool("Open", false);
            }

        }
    }

    public void Initialization()
    {
        EventSystem.current.SetSelectedGameObject(m_slots[0].gameObject);
    }
}
