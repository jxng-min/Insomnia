using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DialogueInfo
{
    public int m_object_id;
    public string[] m_object_dialogue;
}

[System.Serializable]
public class DialgoueInfoList
{
    public DialogueInfo[] m_dialogue_infos;
}

public class DialogueManager : Singleton<DialogueManager>
{
    private bool m_is_ui_active = false;
    public bool IsActive
    {
        get { return m_is_ui_active; }
        private set { m_is_ui_active = value; }
    }

    private string m_dialogue_data_path;
    private Dictionary<int, string[]> m_dialogue_data;

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
    public int CurrentIndex
    {
        get { return m_current_index; }
        set { m_current_index = value; }
    }

    private int m_cumulative_index;
    public int CumulativeIndex
    {
        get { return m_cumulative_index; }
        set { m_cumulative_index = value; }
    }

    private bool m_is_talking = false;
    public bool IsTalking
    {
        get { return m_is_talking; }
        private set { m_is_talking = value; }
    }

    private new void Awake()
    {
        m_dialogue_data_path = Path.Combine(Application.streamingAssetsPath, "DialogueData.json");
        m_dialogue_data = new Dictionary<int, string[]>();

        LoadDialogueData();
    }

    private void LoadDialogueData()
    {
        if(File.Exists(m_dialogue_data_path))
        {
            var json_data = File.ReadAllText(m_dialogue_data_path);
            var dialogue_list = JsonUtility.FromJson<DialgoueInfoList>(json_data);

            if(dialogue_list is not null && dialogue_list.m_dialogue_infos is not null)
            {
                foreach(var dialogue in dialogue_list.m_dialogue_infos)
                {
                    m_dialogue_data.Add(dialogue.m_object_id, dialogue.m_object_dialogue);
                }
            }
        }
        else
        {
            Debug.Log($"{m_dialogue_data_path}가 존재하지 않습니다.");
        }
    }

    public void Initialization()
    {
        m_dialogue_ui_animator = GameObject.Find("Dialogue UI").GetComponent<Animator>();
        m_dialogue_label = GameObject.Find("Dialogue Label").GetComponent<TypeEffectCtrl>();
        m_dialogue_cursor = GameObject.Find("Dialogue Cursor");
    }

    private string GetDialogue(ObjectInfo current_object)
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

    private string GetDialogue(ObjectInfo current_object, int dialogue_index)
    {
        if(dialogue_index == m_dialogue_data[current_object.Data.ID].Length)
        {
            return null;
        }
        else
        {
            return m_dialogue_data.ContainsKey(current_object.Data.ID) ? m_dialogue_data[current_object.Data.ID][dialogue_index] : null;
        }
    }

    public void Dialoging(ObjectInfo current_object)
    {
        var dialogue = GetDialogue(current_object);

        if(dialogue is null)
        {
            m_current_index = 0;
            m_dialogue_ui_animator.SetBool("Open", false);
            IsActive = false;
            IsTalking = false;

            return;
        }

        IsActive = true;
        IsTalking = true;
        m_dialogue_ui_animator.SetBool("Open", true);
        m_dialogue_label.SetText(dialogue);

        m_current_index++;
    }

    public void Dialoging(ObjectInfo current_object, int start_index, int count)
    {
        m_current_index = start_index + CumulativeIndex;

        var dialogue = GetDialogue(current_object, m_current_index);

        if(CumulativeIndex >= count)
        {
            m_current_index = 0;
            CumulativeIndex = 0;
            m_dialogue_ui_animator.SetBool("Open", false);
            IsActive = false;
            IsTalking = false;

            return;
        }

        IsActive = true;
        IsTalking = true;
        m_dialogue_ui_animator.SetBool("Open", true);
        m_dialogue_label.SetText(dialogue);

        m_current_index++;
        CumulativeIndex++;        
    }
}