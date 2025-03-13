using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    private bool m_is_ui_active = false;
    public bool IsActive
    {
        get { return m_is_ui_active; }
        private set { m_is_ui_active = value; }
    }

    [Header("대화창 UI 오브젝트")]
    [SerializeField] private Animator m_dialogue_ui_animator;

    [Header("대화가 작성될 TMP")]
    [SerializeField] private TypeEffectCtrl m_dialogue_label;

    [Header("대화의 종료를 나타낼 커서")]
    [SerializeField] private GameObject m_dialogue_cursor;
    public GameObject Cursor
    {
        get { return m_dialogue_cursor; }
    }

    private int m_current_index;

    public void Initialization()
    {
        m_dialogue_ui_animator = GameObject.Find("Dialogue UI").GetComponent<Animator>();
        m_dialogue_label = GameObject.Find("Dialogue Label").GetComponent<TypeEffectCtrl>();
        m_dialogue_cursor = GameObject.Find("Dialogue Cursor");
    }

    private string GetDialgoue(ObjectInfo current_object)
    {
        if(m_current_index == current_object.Data.Dialogue.Length)
        {
            return null;
        }
        else
        {
            return current_object.Data.Dialogue[m_current_index];
        }
    }

    public void Dialoging(ObjectInfo current_object)
    {
        var dialogue = GetDialgoue(current_object);

        if(dialogue is null)
        {
            m_current_index = 0;
            m_dialogue_ui_animator.SetBool("Open", false);
            IsActive = false;

            return;
        }

        IsActive = true;
        m_dialogue_ui_animator.SetBool("Open", true);
        m_dialogue_label.SetText(dialogue);

        m_current_index++;
    }
}